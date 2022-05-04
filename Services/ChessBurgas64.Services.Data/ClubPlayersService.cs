namespace ChessBurgas64.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.ClubPlayers;

    public class ClubPlayersService : IClubPlayersService
    {
        private readonly IDeletableEntityRepository<ClubPlayer> clubPlayersRepository;
        private readonly IMapper mapper;

        public ClubPlayersService(IDeletableEntityRepository<ClubPlayer> clubPlayersRepository, IMapper mapper)
        {
            this.clubPlayersRepository = clubPlayersRepository;
            this.mapper = mapper;
        }

        public async Task<ClubPlayer> CreateAsync(ClubPlayerInputModel input, string imagePath)
        {
            var clubPlayer = this.mapper.Map<ClubPlayer>(input);

            Directory.CreateDirectory(imagePath);

            await this.clubPlayersRepository.AddAsync(clubPlayer);
            await this.clubPlayersRepository.SaveChangesAsync();

            return clubPlayer;
        }

        public async Task DeleteAsync(int id)
        {
            var clubPlayer = this.clubPlayersRepository.All().FirstOrDefault(x => x.Id == id);
            this.clubPlayersRepository.Delete(clubPlayer);
            await this.clubPlayersRepository.SaveChangesAsync();
        }

        public ICollection<T> GetAll<T>(int page, int itemsPerPage)
        {
            var clubPlayers = this.clubPlayersRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return clubPlayers;
        }

        public T GetById<T>(int id)
        {
            var clubPlayer = this.clubPlayersRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return clubPlayer;
        }

        public ClubPlayer GetById(int id)
        {
            var clubPlayer = this.clubPlayersRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return clubPlayer;
        }

        public int GetCount()
        {
            return this.clubPlayersRepository.AllAsNoTracking().Count();
        }

        public async Task<ClubPlayer> UpdateAsync(int id, ClubPlayerInputModel input)
        {
            var clubPlayer = this.clubPlayersRepository.All().FirstOrDefault(x => x.Id == id);

            clubPlayer.Description = input.Description;
            clubPlayer.FideTitle = input.FideTitle.ToString();
            clubPlayer.Name = input.Name;

            await this.clubPlayersRepository.SaveChangesAsync();

            return clubPlayer;
        }
    }
}

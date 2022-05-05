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
    using ChessBurgas64.Web.ViewModels.NotableMembers;

    public class NotableMembersService : INotableMembersService
    {
        private readonly IDeletableEntityRepository<NotableMember> notableMembersRepository;
        private readonly IMapper mapper;

        public NotableMembersService(IDeletableEntityRepository<NotableMember> notableMembersRepository, IMapper mapper)
        {
            this.notableMembersRepository = notableMembersRepository;
            this.mapper = mapper;
        }

        public async Task<NotableMember> CreateAsync(NotableMemberInputModel input, string imagePath)
        {
            var notableMember = this.mapper.Map<NotableMember>(input);

            Directory.CreateDirectory(imagePath);

            await this.notableMembersRepository.AddAsync(notableMember);
            await this.notableMembersRepository.SaveChangesAsync();

            return notableMember;
        }

        public async Task DeleteAsync(int id)
        {
            var notableMember = this.notableMembersRepository.All().FirstOrDefault(x => x.Id == id);
            this.notableMembersRepository.Delete(notableMember);
            await this.notableMembersRepository.SaveChangesAsync();
        }

        public ICollection<T> GetAllInGovernance<T>()
        {
            var notableMembers = this.notableMembersRepository
                .AllAsNoTracking()
                .Where(x => x.IsPartOfGovernance)
                .OrderBy(x => x.ListIndex)
                .To<T>()
                .ToList();

            return notableMembers;
        }

        public ICollection<T> GetAllPlayers<T>()
        {
            var notableMembers = this.notableMembersRepository
                .AllAsNoTracking()
                .Where(x => !x.IsPartOfGovernance)
                .OrderBy(x => x.ListIndex)
                .To<T>()
                .ToList();

            return notableMembers;
        }

        public T GetById<T>(int id)
        {
            var notableMember = this.notableMembersRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return notableMember;
        }

        public NotableMember GetById(int id)
        {
            var notableMember = this.notableMembersRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return notableMember;
        }

        public int GetCount()
        {
            return this.notableMembersRepository.AllAsNoTracking().Count();
        }

        public async Task<NotableMember> UpdateAsync(int id, NotableMemberInputModel input)
        {
            var notableMember = this.notableMembersRepository.All().FirstOrDefault(x => x.Id == id);

            notableMember.Description = input.Description;
            notableMember.FideTitle = input.FideTitle.ToString();
            notableMember.IsPartOfGovernance = input.IsPartOfGovernance;
            notableMember.ListIndex = input.ListIndex;
            notableMember.Name = input.Name;

            await this.notableMembersRepository.SaveChangesAsync();

            return notableMember;
        }
    }
}

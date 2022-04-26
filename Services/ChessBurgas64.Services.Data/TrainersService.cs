namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Trainers;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TrainersService : ITrainersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Trainer> trainersRepository;
        private readonly IMapper mapper;

        public TrainersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Trainer> trainersRepository,
            IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.trainersRepository = trainersRepository;
            this.mapper = mapper;
        }

        public async Task<Trainer> CreateAsync(TrainerInputModel input, string userId, string imagePath)
        {
            var trainer = this.mapper.Map<Trainer>(input);
            trainer.UserId = userId;

            Directory.CreateDirectory(imagePath);

            await this.trainersRepository.AddAsync(trainer);
            await this.trainersRepository.SaveChangesAsync();

            return trainer;
        }

        public IEnumerable<SelectListItem> GetAllTrainersInSelectList()
        {
            var trainers = this.usersRepository.AllAsNoTracking()
                .Where(u => u.ClubStatus.Equals(ClubStatus.Треньор.ToString()) && u.TrainerId != null)
                .ToList()
                .Select(tr => new
                {
                    tr.TrainerId,
                    Name = $"{tr.FirstName} {tr.LastName}",
                })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.TrainerId));

            return trainers;
        }

        public IEnumerable<T> GetAllTrainersForPublicView<T>(int page, int itemsPerPage)
        {
            var trainers = this.trainersRepository.AllAsNoTracking()
                .OrderByDescending(x => x.User.FideRating)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return trainers;
        }

        public T GetById<T>(string id)
        {
            var trainer = this.trainersRepository.AllAsNoTracking()
                .Where(x => x.UserId == id)
                .To<T>().FirstOrDefault();

            return trainer;
        }

        public int GetCount()
        {
            return this.trainersRepository.AllAsNoTracking().Count();
        }

        public async Task<Trainer> UpdateAsync(string id, TrainerInputModel input, string imagePath)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);

            if (user.TrainerId == null && !this.trainersRepository.AllAsNoTracking().Any(x => x.UserId == id))
            {
                user.Trainer = await this.CreateAsync(input, id, imagePath);
            }
            else
            {
                user.Trainer = this.trainersRepository.All().FirstOrDefault(x => x.UserId == id);
            }

            user.Trainer.DateOfLastAttendance = DateTime.Parse(input.DateOfLastAttendance);
            user.Description = input.UserDescription;

            await this.trainersRepository.SaveChangesAsync();
            await this.usersRepository.SaveChangesAsync();

            return user.Trainer;
        }
    }
}

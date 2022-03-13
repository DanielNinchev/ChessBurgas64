namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Collections.Generic;
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

        public IEnumerable<SelectListItem> GetAllTrainers()
        {
            return this.usersRepository.AllAsNoTracking()
                .Where(tr => tr.ClubStatus.Equals(ClubStatus.Треньор))
                .ToList()
                .Select(tr => new
                {
                    tr.TrainerId,
                    Name = $"{tr.FirstName} {tr.LastName}",
                })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.TrainerId.ToString()));
        }

        public T GetById<T>(string id)
        {
            var trainer = this.trainersRepository.AllAsNoTracking()
                .Where(x => x.UserId == id)
                .To<T>().FirstOrDefault();

            return trainer;
        }

        public async Task UpdateAsync(string id, TrainerInputModel input)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);

            var trainer = this.mapper.Map<Trainer>(input);
            trainer.User = user;
            trainer.UserId = user.Id;

            await this.trainersRepository.AddAsync(trainer);

            user.Trainer = trainer;
            user.TrainerId = trainer.Id;
            user.Trainer.DateOfLastAttendance = DateTime.Parse(input.DateOfLastAttendance);

            await this.trainersRepository.SaveChangesAsync();
            await this.usersRepository.SaveChangesAsync();
        }
    }
}

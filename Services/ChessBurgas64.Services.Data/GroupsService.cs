namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GroupsService : IGroupsService
    {
        private readonly IDeletableEntityRepository<Group> groupsRepository;
        private readonly IDeletableEntityRepository<Trainer> trainersRepository;
        private readonly IMapper mapper;

        public GroupsService(
            IDeletableEntityRepository<Group> groupsRepository,
            IDeletableEntityRepository<Trainer> trainersRepository,
            IMapper mapper)
        {
            this.groupsRepository = groupsRepository;
            this.trainersRepository = trainersRepository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(GroupInputModel input)
        {
            var group = this.mapper.Map<Group>(input);
            await this.groupsRepository.AddAsync(group);
            await this.groupsRepository.SaveChangesAsync();
        }

        //public async Task DeleteAsync(string id)
        //{
        //    var payment = this.groupsRepository.All().FirstOrDefault(x => x.Id == id);

        //    this.groupsRepository.Delete(payment);

        //    await this.groupsRepository.SaveChangesAsync();
        //}

        public IEnumerable<SelectListItem> GetAllGroups()
        {
            return this.groupsRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.Id));
        }

        public T GetById<T>(string id)
        {
            var group = this.groupsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return group;
        }

        public IEnumerable<T> GetTableData<T>(string sortColumn, string sortColumnDirection, string searchValue)
        {
            var groups = this.groupsRepository.All();
            var groupData = from studentGroup in groups select studentGroup;

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                groupData = groupData.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                groupData = groupData.Where(g => g.Name.Contains(searchValue)
                                    || g.LowestRating.Equals(searchValue)
                                    || g.HighestRating.Equals(searchValue)
                                    || g.Members.Count.Equals(searchValue));
            }

            return groupData.To<T>().ToList();
        }

        public async Task UpdateAsync(string id, GroupInputModel input)
        {
            var group = this.groupsRepository.All().FirstOrDefault(x => x.Id == id);
            group.TrainingDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), input.TrainingDay);
            group.TrainingHour = DateTime.Parse(input.TrainingHour);
            group.TrainerId = input.TrainerId;

            await this.groupsRepository.SaveChangesAsync();
        }
    }
}

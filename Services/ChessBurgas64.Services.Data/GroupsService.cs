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
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class GroupsService : IGroupsService
    {
        private readonly IRepository<GroupMember> groupMembersRepository;
        private readonly IDeletableEntityRepository<Group> groupsRepository;
        private readonly IDeletableEntityRepository<Member> membersRepository;
        private readonly IMapper mapper;

        public GroupsService(
            IRepository<GroupMember> groupMembersRepository,
            IDeletableEntityRepository<Group> groupsRepository,
            IDeletableEntityRepository<Member> membersRepository,
            IMapper mapper)
        {
            this.groupMembersRepository = groupMembersRepository;
            this.groupsRepository = groupsRepository;
            this.membersRepository = membersRepository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(GroupInputModel input)
        {
            var group = this.mapper.Map<Group>(input);
            await this.groupsRepository.AddAsync(group);
            await this.groupsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var group = await this.groupsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.groupsRepository.Delete(group);
            await this.groupsRepository.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetAllTrainerGroups(string trainerId)
        {
            var trainerGroups = this.groupsRepository
                .AllAsNoTracking()
                .Where(x => x.TrainerId == trainerId)
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.Id));

            return trainerGroups;
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var group = await this.groupsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return group;
        }

        public async Task<IEnumerable<T>> GetTableDataAsync<T>(string sortColumn, string sortColumnDirection, string searchValue)
        {
            var groups = this.groupsRepository.AllAsNoTracking();
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

            return await groupData.To<T>().ToListAsync();
        }

        public IQueryable<Group> GetUserGroups(string userId)
        {
            var groups = this.groupsRepository
                .AllAsNoTracking()
                .Where(x => x.Members.Any(m => m.Member.UserId == userId) || x.Trainer.UserId == userId);

            return groups;
        }

        public async Task<IEnumerable<T>> GetUserGroupsTableData<T>(string userId, string sortColumn, string sortColumnDirection, string searchValue)
        {
            var groups = this.GetUserGroups(userId);
            var groupData = from studentGroup in groups select studentGroup;

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                groupData = groupData.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                groupData = groupData.Where(g => g.Name.Contains(searchValue)
                                    || g.TrainingHour.Equals(searchValue)
                                    || g.LowestRating.Equals(searchValue)
                                    || g.HighestRating.Equals(searchValue)
                                    || g.Members.Count.Equals(searchValue));
            }

            return await groupData.To<T>().ToListAsync();
        }

        public async Task InitializeGroupProperties(string groupId)
        {
            var group = await this.groupsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == groupId);

            var groupMembers = await this.groupMembersRepository
                .All()
                .Where(x => x.GroupId == groupId && x.MemberId != null)
                .ToListAsync();

            foreach (var groupMember in groupMembers)
            {
                var member = await this.membersRepository
                    .All()
                    .FirstOrDefaultAsync(x => x.Id == groupMember.MemberId);

                groupMember.Member = member;
            }

            this.InitializeLowestClubRatingInGroup(groupMembers, group);
            this.InitializeHighestClubRatingInGroup(groupMembers, group);

            await this.groupMembersRepository.SaveChangesAsync();
            await this.groupsRepository.SaveChangesAsync();
        }

        public void InitializeLowestClubRatingInGroup(List<GroupMember> groupMembers, Group group)
        {
            group.LowestRating = 0;

            foreach (var groupMember in groupMembers)
            {
                if (groupMember.Member != null)
                {
                    if (group.LowestRating > groupMember.Member.ClubRating || group.LowestRating == 0)
                    {
                        group.LowestRating = groupMember.Member.ClubRating;
                    }
                }
            }
        }

        public void InitializeHighestClubRatingInGroup(List<GroupMember> groupMembers, Group group)
        {
            group.HighestRating = 0;

            foreach (var groupMember in groupMembers)
            {
                if (groupMember.Member != null)
                {
                    if (group.HighestRating < groupMember.Member.ClubRating)
                    {
                        group.HighestRating = groupMember.Member.ClubRating;
                    }
                }
            }
        }

        public async Task UpdateAsync(string groupId, GroupInputModel input)
        {
            var group = await this.groupsRepository.All().FirstOrDefaultAsync(x => x.Id == groupId);

            group.TrainingDay = (WeekDay)Enum.Parse(typeof(WeekDay), input.TrainingDay);
            group.TrainingHour = DateTime.Parse(input.TrainingHour);
            group.TrainerId = input.TrainerId;

            await this.InitializeGroupProperties(groupId);
        }
    }
}

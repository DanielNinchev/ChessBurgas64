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
    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using ChessBurgas64.Web.ViewModels.Members;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class MembersService : IMembersService
    {
        private readonly IDeletableEntityRepository<Group> groupsRepository;
        private readonly IRepository<GroupMember> groupMembersRepository;
        private readonly IDeletableEntityRepository<Member> membersRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IMapper mapper;

        public MembersService(
            IDeletableEntityRepository<Group> groupsRepository,
            IRepository<GroupMember> groupMembersRepository,
            IDeletableEntityRepository<Member> membersRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IMapper mapper)
        {
            this.groupsRepository = groupsRepository;
            this.groupMembersRepository = groupMembersRepository;
            this.membersRepository = membersRepository;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public async Task AddMemberToGroupAsync(string groupId, GroupMemberInputModel input)
        {
            var group = this.groupsRepository.All().FirstOrDefault(x => x.Id == groupId);
            var member = this.membersRepository.All().FirstOrDefault(x => x.Id == input.MemberId);

            var groupMember = new GroupMember()
            {
                GroupId = groupId,
                MemberId = member.Id,
            };

            group.Members.Add(groupMember);
            member.Groups.Add(groupMember);

            await this.groupsRepository.SaveChangesAsync();
            await this.groupMembersRepository.SaveChangesAsync();
            await this.membersRepository.SaveChangesAsync();
        }

        public async Task DeleteGroupMemberAsync(string groupId, string memberId)
        {
            var groupMember = this.groupMembersRepository.All()
                .FirstOrDefault(x => x.GroupId == groupId && x.MemberId == memberId);

            this.groupMembersRepository.Delete(groupMember);

            await this.groupMembersRepository.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetAllMembers()
        {
            var members = this.membersRepository.All()
                .Select(m => new
                {
                    m.Id,
                    Name = $"{m.User.FirstName} {m.User.MiddleName} {m.User.LastName}",
                })
                .ToList()
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem(x.Name, x.Id));

            return members;
        }

        public IEnumerable<SelectListItem> GetNecessaryMembers(string groupId)
        {
            var groupMembersExist = this.groupMembersRepository.AllAsNoTracking().Any();

            if (groupMembersExist)
            {
                var members = this.GetAllMembersWhichAreNotInCurrentGroup(groupId);
                return members;
            }
            else
            {
                var members = this.GetAllMembers();
                return members;
            }
        }

        public IEnumerable<SelectListItem> GetAllMembersWhichAreNotInCurrentGroup(string groupId)
        {
            var group = this.groupsRepository.All().FirstOrDefault(x => x.Id == groupId);
            var members = this.membersRepository.All()
                .Where(x => !x.Groups.Any(g => g.GroupId == groupId))
                .Select(m => new
                {
                    m.Id,
                    Name = $"{m.User.FirstName} {m.User.MiddleName} {m.User.LastName}",
                })
                .ToList()
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem(x.Name, x.Id));

            return members;
        }

        public IEnumerable<SelectListItem> GetAllGroupMembers(string groupId)
        {
            var group = this.groupsRepository.All().FirstOrDefault(x => x.Id == groupId);
            var groupMembers = group.Members
                .Where(x => x.GroupId == groupId)
                .Select(grm => new
                {
                    grm.MemberId,
                    Name = $"{grm.Member.User.FirstName} {grm.Member.User.MiddleName} {grm.Member.User.LastName}",
                })
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem(x.Name, x.MemberId));

            return groupMembers;
        }

        public Member GetMemberById(string memberId)
        {
            var member = this.membersRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.Id == memberId);

            return member;
        }

        public T GetByUserId<T>(string userId)
        {
            var member = this.membersRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .To<T>().FirstOrDefault();

            return member;
        }

        public IEnumerable<T> GetTableData<T>(string groupId, string sortColumn, string sortColumnDirection, string searchValue)
        {
            var membersData = this.membersRepository.All()
                .Where(x => x.Groups
                .Any(grm => grm.GroupId == groupId));

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                membersData = membersData.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                membersData = membersData.Where(m => m.DateOfLastAttendance.Equals(searchValue)
                                      || m.ClubRating.Equals(searchValue)
                                      || m.User.FirstName.Contains(searchValue)
                                      || m.User.MiddleName.Contains(searchValue)
                                      || m.User.LastName.Contains(searchValue));
            }

            return membersData.To<T>().ToList();
        }

        public async Task SaveMemberChangesAsync(Member member)
        {
            this.membersRepository.Update(member);
            await this.membersRepository.SaveChangesAsync();
            await this.usersRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(string userId, MemberInputModel input)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);

            var member = this.mapper.Map<Member>(input);
            member.User = user;
            member.UserId = user.Id;

            await this.membersRepository.AddAsync(member);

            user.Member = member;
            user.MemberId = member.Id;

            user.Member.DateOfJoiningTheClub = DateTime.Parse(input.DateOfJoiningTheClub);

            await this.membersRepository.SaveChangesAsync();
            await this.usersRepository.SaveChangesAsync();
        }
    }
}

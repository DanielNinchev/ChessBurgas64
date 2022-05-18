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
    using Microsoft.EntityFrameworkCore;

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
            var groupMember = new GroupMember()
            {
                GroupId = groupId,
                MemberId = input.MemberId,
            };

            await this.groupMembersRepository.AddAsync(groupMember);
            await this.groupsRepository.SaveChangesAsync();
            await this.groupMembersRepository.SaveChangesAsync();
            await this.membersRepository.SaveChangesAsync();
        }

        public async Task DeleteGroupMemberAsync(string groupId, string memberId)
        {
            var groupMember = await this.groupMembersRepository
                .All()
                .FirstOrDefaultAsync(x => x.GroupId == groupId && x.MemberId == memberId);

            this.groupMembersRepository.Delete(groupMember);
            await this.groupMembersRepository.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetAllGroupMembersInSelectList(string groupId)
        {
            var groupMembers = this.groupMembersRepository
                .AllAsNoTracking()
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

        public IEnumerable<SelectListItem> GetAllMembersInSelectList()
        {
            var members = this.membersRepository
                .AllAsNoTracking()
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

        public async Task<Member> GetMemberByIdAsync(string memberId)
        {
            var member = await this.membersRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == memberId);

            return member;
        }

        public IEnumerable<SelectListItem> GetNecessaryMembersInSelectList(string groupId)
        {
            var groupMembersExist = this.groupMembersRepository.AllAsNoTracking().Any();

            if (groupMembersExist)
            {
                var members = this.GetAllMembersWhichAreNotInCurrentGroupInSelectList(groupId);
                return members;
            }
            else
            {
                var members = this.GetAllMembersInSelectList();
                return members;
            }
        }

        public async Task<T> GetByUserIdAsync<T>(string userId)
        {
            var member = await this.membersRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .To<T>()
                .FirstOrDefaultAsync();

            return member;
        }

        public async Task<IEnumerable<T>> GetTableData<T>(string groupId, string sortColumn, string sortColumnDirection, string searchValue)
        {
            var membersData = this.membersRepository
                .All()
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

            return await membersData.To<T>().ToListAsync();
        }

        public async Task SaveMemberChangesAsync(Member member)
        {
            this.membersRepository.Update(member);
            await this.membersRepository.SaveChangesAsync();
            await this.usersRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(string userId, MemberInputModel input)
        {
            var user = await this.usersRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == userId);

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

        private IEnumerable<SelectListItem> GetAllMembersWhichAreNotInCurrentGroupInSelectList(string groupId)
        {
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
    }
}

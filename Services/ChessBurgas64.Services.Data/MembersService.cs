namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Members;

    public class MembersService : IMembersService
    {
        private readonly IDeletableEntityRepository<Member> membersRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IMapper mapper;

        public MembersService(
            IDeletableEntityRepository<Member> membersRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IMapper mapper)
        {
            this.membersRepository = membersRepository;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public T GetById<T>(string id)
        {
            var member = this.membersRepository.AllAsNoTracking()
                .Where(x => x.UserId == id)
                .To<T>().FirstOrDefault();

            return member;
        }

        public async Task UpdateAsync(string id, MemberInputModel input)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);

            var member = this.mapper.Map<Member>(input);
            member.User = user;
            member.UserId = user.Id;

            await this.membersRepository.AddAsync(member);

            user.Member = member;
            user.MemberId = member.Id;
            user.Member.DateOfLastAttendance = DateTime.Parse(input.DateOfLastAttendance);
            user.Member.DateOfJoiningTheClub = DateTime.Parse(input.DateOfJoiningTheClub);

            await this.membersRepository.SaveChangesAsync();
            await this.usersRepository.SaveChangesAsync();
        }
    }
}

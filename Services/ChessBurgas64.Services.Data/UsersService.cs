namespace ChessBurgas64.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Users;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Group> groupsRepository;
        private readonly IDeletableEntityRepository<GroupMember> groupMembersRepository;
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;
        private readonly IDeletableEntityRepository<LessonMember> lessonMembersRepository;
        private readonly IDeletableEntityRepository<Member> membersRepository;
        private readonly IDeletableEntityRepository<Payment> paymentsRepository;
        private readonly IDeletableEntityRepository<Trainer> trainersRepository;
        private readonly IDeletableEntityRepository<Video> videosRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Group> groupsRepository,
            IDeletableEntityRepository<GroupMember> groupMembersRepository,
            IDeletableEntityRepository<Lesson> lessonsRepository,
            IDeletableEntityRepository<LessonMember> lessonMembersRepository,
            IDeletableEntityRepository<Member> membersRepository,
            IDeletableEntityRepository<Payment> paymentsRepository,
            IDeletableEntityRepository<Trainer> trainersRepository,
            IDeletableEntityRepository<Video> videosRepository)
        {
            this.usersRepository = usersRepository;
            this.groupsRepository = groupsRepository;
            this.groupMembersRepository = groupMembersRepository;
            this.lessonsRepository = lessonsRepository;
            this.lessonMembersRepository = lessonMembersRepository;
            this.membersRepository = membersRepository;
            this.paymentsRepository = paymentsRepository;
            this.trainersRepository = trainersRepository;
            this.videosRepository = videosRepository;
        }

        public async Task DeleteAsync(ApplicationUser user)
        {
            await this.DeleteUserMemberAsync(user);
            await this.DeleteUserTrainerAsync(user);

            var userPayments = this.paymentsRepository.All().Where(x => x.UserId == user.Id);
            foreach (var payment in userPayments)
            {
                payment.UserId = null;
                this.paymentsRepository.HardDelete(payment);
            }

            await this.paymentsRepository.SaveChangesAsync();

            this.usersRepository.HardDelete(user);
            await this.usersRepository.SaveChangesAsync();
        }

        public async Task DeleteUserMemberAsync(ApplicationUser user)
        {
            if (user.MemberId != null)
            {
                user.Member = await this.membersRepository
                    .All()
                    .Include(x => x.Groups)
                    .Include(x => x.Lessons)
                    .FirstOrDefaultAsync(x => x.Id == user.MemberId);

                foreach (var groupMember in user.Member.Groups)
                {
                    this.groupMembersRepository.HardDelete(groupMember);
                }

                foreach (var lessonMember in user.Member.Lessons)
                {
                    this.lessonMembersRepository.HardDelete(lessonMember);
                }

                user.Member.UserId = null;
                user.MemberId = null;
                this.membersRepository.HardDelete(user.Member);

                await this.groupMembersRepository.SaveChangesAsync();
                await this.lessonMembersRepository.SaveChangesAsync();
                await this.membersRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteUserTrainerAsync(ApplicationUser user)
        {
            if (user.TrainerId != null)
            {
                user.Trainer = await this.trainersRepository
                    .All()
                    .Include(x => x.Groups)
                    .Include(x => x.Lessons)
                    .Include(x => x.Videos)
                    .FirstOrDefaultAsync(x => x.Id == user.TrainerId);

                foreach (var group in user.Trainer.Groups)
                {
                    group.TrainerId = null;
                }

                foreach (var lesson in user.Trainer.Lessons)
                {
                    lesson.TrainerId = null;
                }

                foreach (var userVideo in user.Trainer.Videos)
                {
                    userVideo.TrainerId = null;
                    this.videosRepository.HardDelete(userVideo);
                }

                user.Trainer.UserId = null;
                user.TrainerId = null;
                this.trainersRepository.HardDelete(user.Trainer);

                await this.groupsRepository.SaveChangesAsync();
                await this.lessonsRepository.SaveChangesAsync();
                await this.videosRepository.SaveChangesAsync();
                await this.trainersRepository.SaveChangesAsync();
            }
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var user = await this.usersRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<T>> GetTableDataAsync<T>(string sortColumn, string sortColumnDirection, string searchValue)
        {
            var users = this.usersRepository.All();
            var userData = from user in users select user;

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                userData = userData.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                userData = userData.Where(s => s.FirstName.Contains(searchValue)
                                    || s.MiddleName.Contains(searchValue)
                                    || s.LastName.Contains(searchValue)
                                    || s.PhoneNumber.Contains(searchValue)
                                    || s.Email.Contains(searchValue));
            }

            return await userData.To<T>().ToListAsync();
        }

        public async Task UpdateAsync(string id, UserInputModel input)
        {
            var user = await this.usersRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            user.ClubStatus = input.ClubStatus.ToString();
            user.FideTitle = input.FideTitle.ToString();
            user.FideRating = input.FideRating;

            if (input.ClubStatus == ClubStatus.Треньор)
            {
                user.Trainer = new Trainer();

                await this.trainersRepository.SaveChangesAsync();
            }

            await this.usersRepository.SaveChangesAsync();
        }
    }
}

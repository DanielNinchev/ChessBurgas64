namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
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
            IDeletableEntityRepository<Image> imagesRepository,
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
            this.imagesRepository = imagesRepository;
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
                user.Member = this.membersRepository.All().FirstOrDefault(x => x.Id == user.MemberId);

                var userGroups = this.groupMembersRepository.All().Where(x => x.MemberId == user.MemberId);
                foreach (var groupMember in userGroups)
                {
                    this.groupMembersRepository.HardDelete(groupMember);
                }

                var userLessons = this.lessonMembersRepository.All().Where(x => x.MemberId == user.MemberId);
                foreach (var lessonMember in userLessons)
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
                user.Trainer = this.trainersRepository.All().FirstOrDefault(x => x.Id == user.TrainerId);

                var userImages = this.imagesRepository.All().Where(x => x.TrainerId == user.TrainerId);
                foreach (var image in userImages)
                {
                    image.TrainerId = null;
                    this.imagesRepository.HardDelete(image);
                }

                var userGroups = this.groupsRepository.All().Where(x => x.TrainerId == user.TrainerId);
                foreach (var group in userGroups)
                {
                    group.TrainerId = null;
                }

                var userLessons = this.lessonsRepository.All().Where(x => x.TrainerId == user.TrainerId);
                foreach (var lesson in userLessons)
                {
                    lesson.TrainerId = null;
                }

                var userVideos = this.videosRepository.All().Where(x => x.TrainerId == user.TrainerId);
                foreach (var userVideo in userVideos)
                {
                    userVideo.TrainerId = null;
                    this.videosRepository.HardDelete(userVideo);
                }

                user.Trainer.UserId = null;
                user.TrainerId = null;
                this.trainersRepository.HardDelete(user.Trainer);

                await this.imagesRepository.SaveChangesAsync();
                await this.groupsRepository.SaveChangesAsync();
                await this.lessonsRepository.SaveChangesAsync();
                await this.videosRepository.SaveChangesAsync();
                await this.trainersRepository.SaveChangesAsync();
            }
        }

        public T GetById<T>(string id)
        {
            var user = this.usersRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return user;
        }

        public IEnumerable<T> GetTableData<T>(string sortColumn, string sortColumnDirection, string searchValue)
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

            return userData.To<T>().ToList();
        }

        public async Task UpdateAsync(string id, UserInputModel input)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);
            user.ClubStatus = input.ClubStatus.ToString();
            user.FideTitle = input.FideTitle.ToString();
            user.FideRating = input.FideRating;

            await this.usersRepository.SaveChangesAsync();
        }
    }
}

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
    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using Microsoft.EntityFrameworkCore;

    public class LessonsService : ILessonsService
    {
        private readonly IDeletableEntityRepository<Group> groupsRepository;
        private readonly IDeletableEntityRepository<GroupMember> groupMembersRepository;
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;
        private readonly IDeletableEntityRepository<LessonMember> lessonMembersRepository;
        private readonly IDeletableEntityRepository<Member> membersRepository;
        private readonly IDeletableEntityRepository<Trainer> trainersRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IMapper mapper;

        public LessonsService(
            IDeletableEntityRepository<Group> groupsRepository,
            IDeletableEntityRepository<GroupMember> groupMembersRepository,
            IDeletableEntityRepository<Lesson> lessonsRepository,
            IDeletableEntityRepository<LessonMember> lessonMembersRepository,
            IDeletableEntityRepository<Member> membersRepository,
            IDeletableEntityRepository<Trainer> trainersRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IMapper mapper)
        {
            this.groupsRepository = groupsRepository;
            this.groupMembersRepository = groupMembersRepository;
            this.lessonsRepository = lessonsRepository;
            this.lessonMembersRepository = lessonMembersRepository;
            this.membersRepository = membersRepository;
            this.trainersRepository = trainersRepository;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(LessonInputModel input, string userId)
        {
            var lesson = this.mapper.Map<Lesson>(input);
            await this.lessonsRepository.AddAsync(lesson);

            var trainer = await this.trainersRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            var group = await this.groupsRepository
                .AllAsNoTracking()
                .Include(x => x.Members)
                .FirstOrDefaultAsync(x => x.Id == input.GroupId);

            trainer.Lessons.Add(lesson);
            group.Lessons.Add(lesson);
            lesson.TrainerId = trainer.Id;
            lesson.GroupId = group.Id;

            if (group.Members != null)
            {
                foreach (var groupMember in group.Members)
                {
                    var lessonMember = new LessonMember()
                    {
                        LessonId = lesson.Id,
                        MemberId = groupMember.MemberId,
                    };
                }
            }

            await this.groupsRepository.SaveChangesAsync();
            await this.lessonsRepository.SaveChangesAsync();
            await this.lessonMembersRepository.SaveChangesAsync();
            await this.trainersRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lesson = await this.lessonsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.lessonsRepository.Delete(lesson);
            await this.lessonsRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var lesson = await this.lessonsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return lesson;
        }

        public async Task<IEnumerable<T>> GetLessonGroupMembersAsync<T>(int id)
        {
            return await this.groupMembersRepository
                .AllAsNoTracking()
                .Where(x => x.Group.Lessons.Any(x => x.Id == id))
                .OrderBy(x => x.Member.User.FirstName)
                .ThenBy(x => x.Member.User.MiddleName)
                .ThenBy(x => x.Member.User.LastName)
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllLessonsTableDataAsync<T>(string sortColumn, string sortColumnDirection, string searchValue)
        {
            var lessons = this.lessonsRepository.AllAsNoTracking();
            var lessonData = from lesson in lessons select lesson;

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                lessonData = lessonData.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                lessonData = lessonData.Where(l => l.Topic.Contains(searchValue)
                                    || l.StartingTime.Equals(searchValue)
                                    || l.Group.Name.Contains(searchValue)
                                    || l.Trainer.User.FirstName.Contains(searchValue)
                                    || l.Trainer.User.LastName.Contains(searchValue));
            }

            return await lessonData.To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetGroupLessonsTableDataAsync<T>(string groupId, string sortColumn, string sortColumnDirection, string searchValue)
        {
            var lessons = this.lessonsRepository.AllAsNoTracking().Where(x => x.GroupId == groupId);
            var lessonData = from lesson in lessons select lesson;

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                lessonData = lessonData.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                lessonData = lessonData.Where(l => l.Topic.Contains(searchValue)
                                    || l.StartingTime.Equals(searchValue)
                                    || l.Trainer.User.FirstName.Contains(searchValue)
                                    || l.Trainer.User.LastName.Contains(searchValue));
            }

            return await lessonData.To<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetUserLessonsTableDataAsync<T>(string userId, string sortColumn, string sortColumnDirection, string searchValue)
        {
            var lessons = this.lessonsRepository
                .AllAsNoTracking()
                .Where(x => x.Trainer.UserId == userId || x.Members.Any(m => m.Member.UserId == userId));

            var lessonData = from lesson in lessons select lesson;

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                lessonData = lessonData.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                lessonData = lessonData.Where(l => l.Topic.Contains(searchValue)
                                    || l.StartingTime.Equals(searchValue)
                                    || l.Group.Name.Contains(searchValue)
                                    || l.Trainer.User.FirstName.Contains(searchValue)
                                    || l.Trainer.User.LastName.Contains(searchValue));
            }

            return await lessonData.To<T>().ToListAsync();
        }

        public IQueryable<Lesson> GetUserLessonsTableData(string userId)
        {
            var lesson = this.lessonsRepository
                .AllAsNoTracking()
                .Where(x => x.Members.Any(m => m.Member.UserId == userId) || x.Trainer.UserId == userId);

            return lesson;
        }

        public async Task MarkLessonMemberAttendanceAsync(int id, GroupMemberCheckboxModel model)
        {
            var lesson = await this.lessonsRepository
                .All()
                .Include(x => x.Members
                    .OrderBy(x => x.Member.User.FirstName)
                    .ThenBy(x => x.Member.User.MiddleName)
                    .ThenByDescending(x => x.Member.User.LastName))
                .ThenInclude(l => l.Member)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (lesson.Members.Count > 0)
            {
                foreach (var lessonMember in lesson.Members)
                {
                    await this.DeleteLessonMembersAsync(lessonMember);
                }
            }

            var groupMembers = model.GroupMembers
                .Where(x => x.Selected)
                .OrderBy(x => x.Member.UserFirstName)
                .ThenBy(x => x.Member.UserMiddleName)
                .ThenBy(x => x.Member.UserLastName);

            foreach (var groupMember in groupMembers)
            {
                var lessonMember = new LessonMember
                {
                    LessonId = id,
                    MemberId = groupMember.MemberId,
                };

                var member = await this.membersRepository.All().FirstOrDefaultAsync(x => x.Id == groupMember.MemberId);

                member.Lessons.Add(lessonMember);
                lesson.Members.Add(lessonMember);
            }

            await this.lessonMembersRepository.SaveChangesAsync();
            await this.lessonsRepository.SaveChangesAsync();
            await this.membersRepository.SaveChangesAsync();

            await this.MarkLastAttendancesAsync(lesson);
        }

        public async Task UpdateAsync(int id, LessonInputModel input)
        {
            var lesson = await this.lessonsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            lesson.Topic = input.Topic;
            lesson.StartingTime = DateTime.Parse(input.StartingTime);
            lesson.Notes = input.Notes;
            lesson.GroupId = input.GroupId;
            lesson.TrainerId = input.TrainerId;

            await this.lessonsRepository.SaveChangesAsync();
        }

        private async Task DeleteLessonMembersAsync(LessonMember lessonMember)
        {
            this.lessonMembersRepository.HardDelete(lessonMember);
            await this.lessonMembersRepository.SaveChangesAsync();
            await this.lessonsRepository.SaveChangesAsync();
        }

        private async Task MarkLastAttendancesAsync(Lesson lesson)
        {
            foreach (var member in lesson.Members)
            {
                member.Member.DateOfLastAttendance = lesson.StartingTime;
            }

            await this.membersRepository.SaveChangesAsync();
        }
    }
}

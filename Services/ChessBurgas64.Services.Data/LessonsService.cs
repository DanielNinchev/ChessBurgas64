﻿namespace ChessBurgas64.Services.Data
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

            var trainer = this.trainersRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == userId);
            trainer.Lessons.Add(lesson);

            var group = this.groupsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == input.GroupId);
            group.Lessons.Add(lesson);

            lesson.TrainerId = trainer.Id;
            lesson.GroupId = group.Id;

            var groupMembers = this.groupMembersRepository.AllAsNoTracking()
                .Where(x => x.GroupId == group.Id)
                .ToList();

            if (groupMembers != null)
            {
                foreach (var groupMember in groupMembers)
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
            var lesson = this.lessonsRepository.All().FirstOrDefault(x => x.Id == id);
            this.lessonsRepository.Delete(lesson);
            await this.lessonsRepository.SaveChangesAsync();
        }

        public async Task DeleteLessonMembersAsync(int id)
        {
            var lessonMember = this.lessonMembersRepository.AllAsNoTracking().First(x => x.LessonId == id);
            this.lessonMembersRepository.HardDelete(lessonMember);
            await this.lessonMembersRepository.SaveChangesAsync();
            await this.lessonsRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            var lesson = this.lessonsRepository.AllAsNoTracking().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return lesson;
        }

        public List<T> GetLessonGroupMembers<T>(int id)
        {
            var lessonGroup = this.groupsRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.Lessons.Any(l => l.Id == id));

            var lessonGroupMembers = this.groupMembersRepository.AllAsNoTracking()
                .Where(x => x.GroupId == lessonGroup.Id)
                .OrderBy(x => x.Member.User.FirstName)
                .ThenBy(x => x.Member.User.MiddleName)
                .ThenBy(x => x.Member.User.LastName)
                .To<T>()
                .ToList();

            return lessonGroupMembers;
        }

        public IEnumerable<T> GetAllLessonsTableData<T>(string sortColumn, string sortColumnDirection, string searchValue)
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

            return lessonData.To<T>().ToList();
        }

        public IEnumerable<T> GetGroupLessonsTableData<T>(string groupId, string sortColumn, string sortColumnDirection, string searchValue)
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

            return lessonData.To<T>().ToList();
        }

        public IEnumerable<T> GetTrainerLessonsTableData<T>(string userId, string sortColumn, string sortColumnDirection, string searchValue)
        {
            var user = this.usersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == userId);
            IQueryable<Lesson> lessons;

            if (user.ClubStatus == ClubStatus.Треньор.ToString())
            {
                var trainer = this.trainersRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == userId);
                lessons = this.lessonsRepository.AllAsNoTracking().Where(l => l.TrainerId == trainer.Id);
            }
            else
            {
                lessons = this.lessonsRepository.AllAsNoTracking().Where(l => l.Members.Any(x => x.MemberId == user.MemberId));
            }

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

            return lessonData.To<T>().ToList();
        }

        public IQueryable<Lesson> GetUserLessonsTableData(string userId)
        {
            var lesson = this.lessonsRepository
                .AllAsNoTracking()
                .Where(x => x.Members.Any(m => m.Member.UserId == userId) || x.Trainer.UserId == userId);

            return lesson;
        }

        public async Task MarkLessonMemberAttendance(int id, GroupMemberCheckboxModel model)
        {
            var lesson = this.lessonsRepository.All().FirstOrDefault(x => x.Id == id);
            var lessonMembers = this.lessonMembersRepository.AllAsNoTracking()
                .Where(x => x.LessonId == id)
                .ToList();

            if (lessonMembers.Count > 0)
            {
                for (int i = 0; i < lessonMembers.Count; i++)
                {
                    await this.DeleteLessonMembersAsync(id);
                }
            }

            foreach (var groupMember in model.GroupMembers.Where(x => x.Selected))
            {
                var lessonMember = new LessonMember
                {
                    LessonId = id,
                    MemberId = groupMember.MemberId,
                };

                var member = this.membersRepository.All().FirstOrDefault(x => x.Id == groupMember.MemberId);

                member.Lessons.Add(lessonMember);
                lesson.Members.Add(lessonMember);
            }

            await this.lessonMembersRepository.SaveChangesAsync();
            await this.lessonsRepository.SaveChangesAsync();
            await this.membersRepository.SaveChangesAsync();

            await this.MarkLastAttendances(id);
        }

        public async Task MarkLastAttendances(int lessonId)
        {
            var members = this.membersRepository.All().Where(x => x.Lessons.Any(l => l.LessonId == lessonId));

            foreach (var member in members)
            {
                var lessons = this.lessonMembersRepository.AllAsNoTracking()
                    .Where(x => x.MemberId == member.Id)
                    .OrderByDescending(x => x.Lesson.StartingTime);

                var memberLessonId = lessons.FirstOrDefault(x => x.MemberId == member.Id).LessonId;
                var memberLesson = this.GetById<LessonViewModel>(memberLessonId);
                member.DateOfLastAttendance = memberLesson.StartingTime;
            }

            await this.membersRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, LessonInputModel input)
        {
            var lesson = this.lessonsRepository.All().FirstOrDefault(x => x.Id == id);

            lesson.Topic = input.Topic;
            lesson.StartingTime = DateTime.Parse(input.StartingTime);
            lesson.Notes = input.Notes;
            lesson.GroupId = input.GroupId;
            lesson.TrainerId = input.TrainerId;

            await this.lessonsRepository.SaveChangesAsync();
        }
    }
}

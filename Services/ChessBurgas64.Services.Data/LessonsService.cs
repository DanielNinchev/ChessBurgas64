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
    using ChessBurgas64.Web.ViewModels.Lessons;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class LessonsService : ILessonsService
    {
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;
        private readonly IMapper mapper;

        public LessonsService(IDeletableEntityRepository<Lesson> lessonsRepository, IMapper mapper)
        {
            this.lessonsRepository = lessonsRepository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(LessonInputModel input, string groupId)
        {
            var lesson = this.mapper.Map<Lesson>(input);

            await this.lessonsRepository.AddAsync(lesson);
            await this.lessonsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lesson = this.lessonsRepository.All().FirstOrDefault(x => x.Id == id);

            this.lessonsRepository.Delete(lesson);

            await this.lessonsRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            var lesson = this.lessonsRepository.AllAsNoTracking().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return lesson;
        }

        public IEnumerable<T> GetTableData<T>(string groupId, string sortColumn, string sortColumnDirection, string searchValue)
        {
            var lessons = this.lessonsRepository.All().Where(p => p.GroupId == groupId);
            var lessonData = from lesson in lessons select lesson;

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                lessonData = lessonData.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                // TODO: add search by date and amount
                lessonData = lessonData.Where(l => l.Topic.Contains(searchValue)
                                    || l.StartingTime.Equals(searchValue)
                                    || l.Group.Name.Contains(searchValue)
                                    || l.Trainer.User.FirstName.Contains(searchValue)
                                    || l.Trainer.User.LastName.Contains(searchValue));
            }

            return lessonData.To<T>().ToList();
        }

        public async Task UpdateAsync(int id, LessonInputModel input)
        {
            var lesson = this.lessonsRepository.All().FirstOrDefault(x => x.Id == id);

            lesson.Topic = input.Topic;
            lesson.StartingTime = DateTime.Parse(input.StartingTime);
            lesson.GroupId = input.GroupId;
            lesson.TrainerId = input.TrainerId;

            await this.lessonsRepository.SaveChangesAsync();
        }
    }
}

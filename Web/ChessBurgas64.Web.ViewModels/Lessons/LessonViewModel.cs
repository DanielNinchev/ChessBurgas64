namespace ChessBurgas64.Web.ViewModels.Lessons
{
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class LessonViewModel : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string StartingTime { get; set; }

        public string GroupId { get; set; }

        public string GroupName { get; set; }

        public string TrainerId { get; set; }

        public string TrainerUserFirstName { get; set; }

        public string TrainerUserLastName { get; set; }
    }
}

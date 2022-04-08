namespace ChessBurgas64.Web.ViewModels.Lessons
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;
    using ChessBurgas64.Web.ViewModels.LessonMembers;

    public class LessonViewModel : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public DateTime StartingTime { get; set; }

        public string Notes { get; set; }

        public string GroupName { get; set; }

        public string GroupTrainerUserFirstName { get; set; }

        public string GroupTrainerUserLastName { get; set; }

        [NotMapped]
        public string TrainerName => $"{this.GroupTrainerUserFirstName} {this.GroupTrainerUserLastName}";

        public GroupViewModel Group { get; set; }

        public int MembersCount { get; set; }

        public ICollection<LessonMemberViewModel> Members { get; set; }
    }
}

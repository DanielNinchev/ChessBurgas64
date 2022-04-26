namespace ChessBurgas64.Web.ViewModels.Groups
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class GroupTableViewModel : IMapFrom<Group>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TrainingDay { get; set; }

        public DateTime TrainingHour { get; set; }

        public int LowestRating { get; set; }

        public int HighestRating { get; set; }

        public int MembersCount { get; set; }

        public string TrainerUserFirstName { get; set; }

        public string TrainerUserLastName { get; set; }

        [NotMapped]
        public string TrainerName => $"{this.TrainerUserFirstName} {this.TrainerUserLastName}";
    }
}

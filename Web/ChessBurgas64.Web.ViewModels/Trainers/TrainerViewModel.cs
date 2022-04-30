namespace ChessBurgas64.Web.ViewModels.Trainers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;
    using ChessBurgas64.Web.ViewModels.Members;
    using ChessBurgas64.Web.ViewModels.Users;

    public class TrainerViewModel : IdHoldingModel, IMapFrom<Trainer>
    {
        public DateTime DateOfLastAttendance { get; set; }

        public string ImageImageUrl { get; set; }

        public UserTableViewModel User { get; set; }

        public string UserDescription { get; set; }

        public int UserFideRating { get; set; }

        public virtual ICollection<GroupViewModel> Groups { get; set; }

        public virtual ICollection<MemberViewModel> IndividualStudents { get; set; }

        [NotMapped]
        public string TrainerName => $"{this.User.FirstName} {this.User.LastName}";
    }
}

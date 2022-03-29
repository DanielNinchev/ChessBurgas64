namespace ChessBurgas64.Web.ViewModels.Trainers
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;
    using ChessBurgas64.Web.ViewModels.Members;
    using ChessBurgas64.Web.ViewModels.Users;

    public class TrainerViewModel : IMapFrom<Trainer>
    {
        public DateTime DateOfLastAttendance { get; set; }

        public string ImageImageUrl { get; set; }

        public UserTableViewModel User { get; set; }

        public virtual ICollection<GroupViewModel> Groups { get; set; }

        public virtual ICollection<MemberViewModel> IndividualStudents { get; set; }
    }
}

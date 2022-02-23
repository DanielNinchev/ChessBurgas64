namespace ChessBurgas64.Web.ViewModels.Members
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Users;

    public class MemberViewModel : UserTableViewModel, IMapFrom<Member>
    {
        public string Address { get; set; }

        public string School { get; set; }

        public DateTime DateOfJoiningTheClub { get; set; }

        public DateTime DateOfLastAttendance { get; set; }

        public virtual ICollection<string> LearnedOpenings { get; set; }

        public int ClubRating { get; set; }

        public int? LastPuzzleLevel { get; set; }

        public DateTime DateOfJoiningCurrentGroup { get; set; }
    }
}

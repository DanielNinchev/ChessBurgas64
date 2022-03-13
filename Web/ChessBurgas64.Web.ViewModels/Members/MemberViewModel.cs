namespace ChessBurgas64.Web.ViewModels.Members
{
    using System;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;
    using ChessBurgas64.Web.ViewModels.Users;

    public class MemberViewModel : IMapFrom<Member>
    {
        public string Address { get; set; }

        public string School { get; set; }

        public DateTime DateOfJoiningTheClub { get; set; }

        public DateTime DateOfLastAttendance { get; set; }

        public string LearnedOpenings { get; set; }

        public int ClubRating { get; set; }

        public int? LastPuzzleLevel { get; set; }

        public DateTime DateOfJoiningCurrentGroup { get; set; }

        public GroupViewModel Group { get; set; }

        public UserTableViewModel User { get; set; }
    }
}

namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class Member : BaseDeletableModel<string>
    {
        public Member()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Groups = new HashSet<GroupMember>();
            this.Lessons = new HashSet<LessonMember>();
            this.Puzzles = new HashSet<PuzzleMember>();
        }

        [PersonalData]
        public string Address { get; set; }

        [PersonalData]
        public string School { get; set; }

        [PersonalData]
        public DateTime DateOfJoiningTheClub { get; set; }

        public DateTime DateOfLastAttendance { get; set; }

        public int ClubRating { get; set; }

        public int? LastPuzzleLevel { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<GroupMember> Groups { get; set; }

        public virtual ICollection<LessonMember> Lessons { get; set; }

        public virtual ICollection<PuzzleMember> Puzzles { get; set; }
    }
}

namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ChessBurgas64.Data.Common.Models;

    public class Member : BaseDeletableModel<string>
    {
        public Member()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Groups = new HashSet<GroupMember>();
            this.Lessons = new HashSet<LessonMember>();
            this.Puzzles = new HashSet<PuzzleMember>();
            this.Tournaments = new HashSet<TournamentMember>();
        }

        public string Address { get; set; }

        public string School { get; set; }

        public DateTime DateOfJoiningTheClub { get; set; }

        public DateTime DateOfLastAttendance { get; set; }

        public string LearnedOpenings { get; set; }

        public int ClubRating { get; set; }

        public int? LastPuzzleLevel { get; set; }

        public DateTime DateOfJoiningCurrentGroup { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<GroupMember> Groups { get; set; }

        public virtual ICollection<LessonMember> Lessons { get; set; }

        public virtual ICollection<PuzzleMember> Puzzles { get; set; }

        public virtual ICollection<TournamentMember> Tournaments { get; set; }
    }
}

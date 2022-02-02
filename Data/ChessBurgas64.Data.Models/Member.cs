namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Data.Common.Models;

    public class Member : BaseDeletableModel<string>
    {
        public Member()
        {
            this.Payments = new HashSet<Payment>();
            this.Lessons = new HashSet<LessonMember>();
            this.Puzzles = new HashSet<PuzzleMember>();
            this.Tournaments = new HashSet<TournamentMember>();
            this.LearnedOpenings = new HashSet<string>();
        }

        public string Address { get; set; }

        public string School { get; set; }

        public DateTime DateOfJoiningTheClub { get; set; }

        public DateTime DateOfLastAttendance { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

        public virtual ICollection<LessonMember> Lessons { get; set; }

        public virtual ICollection<PuzzleMember> Puzzles { get; set; }

        public virtual ICollection<TournamentMember> Tournaments { get; set; }

        [NotMapped]
        public virtual ICollection<string> LearnedOpenings { get; set; }

        public int ClubRating { get; set; }

        public int? LastPuzzleLevel { get; set; }

        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }

        public DateTime DateOfJoiningCurrentGroup { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

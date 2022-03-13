﻿namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class Trainer : BaseDeletableModel<string>
    {
        public Trainer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Lessons = new HashSet<Lesson>();
            this.Groups = new HashSet<Group>();
            this.IndividualStudents = new HashSet<Member>();
        }

        public DateTime DateOfLastAttendance { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<Member> IndividualStudents { get; set; }
    }
}

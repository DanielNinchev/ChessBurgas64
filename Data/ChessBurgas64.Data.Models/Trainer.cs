namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;
    using ChessBurgas64.Data.Models.Enums;

    public class Trainer : BaseDeletableModel<string>
    {
        public Trainer()
        {
            this.Payments = new HashSet<Payment>();
            this.Lessons = new HashSet<Lesson>();
            this.Groups = new HashSet<Group>();
            this.IndividualStudents = new HashSet<Member>();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfLastAttendance { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Member> IndividualStudents { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

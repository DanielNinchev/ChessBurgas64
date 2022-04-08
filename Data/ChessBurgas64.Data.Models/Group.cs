namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;
    using ChessBurgas64.Data.Models.Enums;

    public class Group : BaseDeletableModel<string>
    {
        public Group()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Lessons = new HashSet<Lesson>();
            this.Members = new HashSet<GroupMember>();
        }

        public string Name { get; set; }

        public int LowestRating { get; set; }

        public int HighestRating { get; set; }

        public WeekDay TrainingDay { get; set; }

        public DateTime TrainingHour { get; set; }

        public string TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<GroupMember> Members { get; set; }
    }
}

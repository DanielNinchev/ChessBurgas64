namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class Lesson : BaseDeletableModel<int>
    {
        public Lesson()
        {
            this.Members = new HashSet<LessonMember>();
        }

        public string Topic { get; set; }

        public DateTime StartingTime { get; set; }

        public string Notes { get; set; }

        public string TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }

        public string GroupId { get; set; }

        public virtual Group Group { get; set; }

        public virtual ICollection<LessonMember> Members { get; set; }
    }
}

namespace ChessBurgas64.Data.Models
{
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class Group : BaseDeletableModel<int>
    {
        public Group()
        {
            this.Lessons = new HashSet<LessonGroup>();
            this.Members = new HashSet<Member>();
        }

        public string Name { get; set; }

        public int LowestRating { get; set; }

        public int HighestRating { get; set; }

        public string TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }

        public virtual ICollection<LessonGroup> Lessons { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}

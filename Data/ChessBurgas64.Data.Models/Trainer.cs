namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class Trainer : BaseDeletableModel<string>
    {
        public Trainer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Groups = new HashSet<Group>();
            this.Lessons = new HashSet<Lesson>();
            this.Videos = new HashSet<Video>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
}

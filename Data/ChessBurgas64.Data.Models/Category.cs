namespace ChessBurgas64.Data.Models
{
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Announcements = new HashSet<Announcement>();
            this.Puzzles = new HashSet<Puzzle>();
            this.Tournaments = new HashSet<Tournament>();
        }

        public string Name { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }

        public virtual ICollection<Puzzle> Puzzles { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}

namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class Announcement : BaseDeletableModel<int>
    {
        public Announcement()
        {
            this.Images = new HashSet<Image>();
        }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        public string MainImageUrl { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}

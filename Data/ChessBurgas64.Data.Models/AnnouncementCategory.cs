namespace ChessBurgas64.Data.Models
{
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class AnnouncementCategory : BaseDeletableModel<int>
    {
        public AnnouncementCategory()
        {
            this.Announcements = new HashSet<Announcement>();
        }

        public string Name { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }
    }
}

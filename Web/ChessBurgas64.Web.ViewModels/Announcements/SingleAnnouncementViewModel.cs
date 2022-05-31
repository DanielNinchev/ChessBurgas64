namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using System.Collections.Generic;

    using ChessBurgas64.Data.Models;

    public class SingleAnnouncementViewModel : AnnouncementInCardViewModel
    {
        public int Views { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}

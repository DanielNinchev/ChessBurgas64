namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using System.Collections.Generic;

    public class AnnouncementsListViewModel : EntityInListViewModel
    {
        public IEnumerable<AnnouncementInCardViewModel> Announcements { get; set; }
    }
}

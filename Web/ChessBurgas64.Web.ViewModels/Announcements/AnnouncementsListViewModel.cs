namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using System.Collections.Generic;

    public class AnnouncementsListViewModel : PublicEntityInListViewModel
    {
        public IEnumerable<AnnouncementInCardViewModel> Announcements { get; set; }
    }
}

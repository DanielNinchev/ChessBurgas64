namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class EditAnnouncementInputModel : BaseAnnouncementInputModel, IMapFrom<Announcement>
    {
        public int Id { get; set; }
    }
}

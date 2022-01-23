namespace ChessBurgas64.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;

    using ChessBurgas64.Web.ViewModels.Announcements;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateAnnouncementInputModel : AnnouncementViewModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}

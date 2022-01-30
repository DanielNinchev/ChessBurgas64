namespace ChessBurgas64.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Web.ViewModels.Announcements;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateAnnouncementInputModel : AnnouncementViewModel
    {
        [Required]
        public IFormFile MainImage { get; set; }

        public IEnumerable<IFormFile> AdditionalImages { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}

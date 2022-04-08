namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AnnouncementInputModel : IMapFrom<Announcement>
    {
        [Required]
        [MinLength(GlobalConstants.AnnouncementTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(GlobalConstants.AnnouncementTextMinLength)]
        public string Text { get; set; }

        [Required]
        [MinLength(GlobalConstants.AnnouncementDescriptionMinLength)]
        [MaxLength(GlobalConstants.AnnouncementDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public IFormFile MainImage { get; set; }

        public IEnumerable<IFormFile> AdditionalImages { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}

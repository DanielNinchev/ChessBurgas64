namespace ChessBurgas64.Web.ViewModels.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Web.ViewModels.Announcements;
    using Microsoft.AspNetCore.Http;

    public class CreateAnnouncementInputModel : BaseAnnouncementInputModel
    {
        [Required]
        public IFormFile MainImage { get; set; }

        public IEnumerable<IFormFile> AdditionalImages { get; set; }
    }
}

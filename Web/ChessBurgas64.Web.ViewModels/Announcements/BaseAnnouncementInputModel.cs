namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public abstract class BaseAnnouncementInputModel
    {
        [Required]
        [MinLength(4)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Text { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string MainImageUrl { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}

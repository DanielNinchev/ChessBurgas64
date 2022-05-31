namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AnnouncementInputModel : CategoryInputModel, IMapFrom<Announcement>
    {
        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [StringLength(
            GlobalConstants.AnnouncementTitleMaxLength,
            ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters,
            MinimumLength = GlobalConstants.AnnouncementTitleMinLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [MinLength(GlobalConstants.AnnouncementTextMinLength)]
        public string Text { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string AuthorId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Authors { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [StringLength(
            GlobalConstants.AnnouncementDescriptionMaxLength,
            ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters,
            MinimumLength = GlobalConstants.AnnouncementDescriptionMinLength)]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public IFormFile MainImage { get; set; }

        public IEnumerable<IFormFile> AdditionalImages { get; set; }
    }
}

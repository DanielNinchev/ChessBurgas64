namespace ChessBurgas64.Web.ViewModels.Videos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class VideoInputModel : CategoryInputModel, IMapFrom<Video>
    {
        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [StringLength(
            GlobalConstants.AnnouncementTitleMaxLength,
            ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters,
            MinimumLength = GlobalConstants.AnnouncementTitleMinLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [StringLength(
            GlobalConstants.AnnouncementTitleMaxLength,
            ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters,
            MinimumLength = GlobalConstants.AnnouncementTitleMinLength)]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [Url(ErrorMessage = ErrorMessages.InvalidUrl)]
        public string Url { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string TrainerId { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }
    }
}

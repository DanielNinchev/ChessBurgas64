namespace ChessBurgas64.Web.ViewModels.Lessons
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class LessonInputModel : IMapFrom<Lesson>
    {
        [Required]
        [MaxLength(GlobalConstants.TopicMaxLength)]
        public string Topic { get; set; }

        [Required]
        [Display(Name = GlobalConstants.DateAndTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public string StartingTime { get; set; }

        public string Notes { get; set; }

        [Required]
        public string GroupId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Groups { get; set; }

        [Required]
        public string TrainerId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Trainers { get; set; }
    }
}

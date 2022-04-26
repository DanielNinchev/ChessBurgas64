namespace ChessBurgas64.Web.ViewModels.Groups
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GroupInputModel : IMapFrom<Group>
    {
        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string TrainingDay { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string TrainingHour { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string TrainerId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Trainers { get; set; }
    }
}

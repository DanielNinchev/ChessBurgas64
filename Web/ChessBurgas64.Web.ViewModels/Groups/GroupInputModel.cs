namespace ChessBurgas64.Web.ViewModels.Groups
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GroupInputModel : IMapFrom<Group>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string TrainingDay { get; set; }

        [Required]
        public string TrainingHour { get; set; }

        [Required]
        public string TrainerId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Trainers { get; set; }
    }
}

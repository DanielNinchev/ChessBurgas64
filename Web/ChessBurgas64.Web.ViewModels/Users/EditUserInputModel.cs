namespace ChessBurgas64.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditUserInputModel : UserTableViewModel, IMapFrom<ApplicationUser>
    {
        [Display(Name = GlobalConstants.DateOfJoiningTheClub)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfJoiningTheClub { get; set; }

        [Display(Name = GlobalConstants.LastAttendance)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfLastAttendance { get; set; }

        public virtual ICollection<string> LearnedOpenings { get; set; }

        [Range(500, 3000)]
        public int ClubRating { get; set; }

        [Range(1, int.MaxValue)]
        public int? LastPuzzleLevel { get; set; }

        [Display(Name = GlobalConstants.DateOfJoingCurrentGroup)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfJoiningCurrentGroup { get; set; }

        public IEnumerable<SelectListItem> ClubStatuses { get; set; }
    }
}

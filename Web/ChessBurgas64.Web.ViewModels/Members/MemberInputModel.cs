namespace ChessBurgas64.Web.ViewModels.Members
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class MemberInputModel : IMapFrom<Member>
    {
        [Range(500, 3000)]
        public int ClubRating { get; set; }

        [Range(1, int.MaxValue)]
        public int? LastPuzzleLevel { get; set; }

        [Display(Name = GlobalConstants.DateOfJoiningTheClub)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public string DateOfJoiningTheClub { get; set; }

        [Display(Name = GlobalConstants.LastAttendance)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public string DateOfLastAttendance { get; set; }

        [Display(Name = GlobalConstants.DateOfJoiningCurrentGroup)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfJoiningCurrentGroup { get; set; }

        public string LearnedOpenings { get; set; }
    }
}

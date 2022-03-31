namespace ChessBurgas64.Web.ViewModels.Members
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;

    public class MemberProfileModel : IMapFrom<Member>
    {
        [StringLength(GlobalConstants.AddressMaxLength, ErrorMessage = "Моля, въвеждайте истински адрес! Адресът не може да съдържа по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.AddressMinLength)]
        [Display(Name = GlobalConstants.Address)]
        public string Address { get; set; }

        [StringLength(GlobalConstants.SchoolMaxLength, ErrorMessage = "Моля, въвеждайте истинскo училище! Училището не може да съдържа по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.SchoolMinLength)]
        [Display(Name = GlobalConstants.School)]
        public string School { get; set; }

        [Display(Name = GlobalConstants.DateOfJoiningTheClub)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public string DateOfJoiningTheClub { get; set; }

        public string LearnedOpenings { get; set; }

        public int ClubRating { get; set; }

        public int? LastPuzzleLevel { get; set; }

        public DateTime DateOfJoiningCurrentGroup { get; set; }

        public GroupViewModel Group { get; set; }
    }
}

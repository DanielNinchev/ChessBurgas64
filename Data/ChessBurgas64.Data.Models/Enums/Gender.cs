namespace ChessBurgas64.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;

    public enum Gender
    {
        Other = 0,

        [Display(Name = GlobalConstants.Male)]
        Male = 1,

        [Display(Name = GlobalConstants.Female)]
        Female = 2,
    }
}

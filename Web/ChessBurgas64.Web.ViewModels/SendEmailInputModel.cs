namespace ChessBurgas64.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;
    using Microsoft.AspNetCore.Mvc;

    public class SendEmailInputModel
    {
        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [Display(Name = GlobalConstants.FirstName)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [RegularExpression(@"^([0-9])$", ErrorMessage = ErrorMessages.InvalidPhoneNumber)]
        [Display(Name = GlobalConstants.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [Display(Name = GlobalConstants.Topic)]
        public string Topic { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [Display(Name = GlobalConstants.Message)]
        public string Message { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
    }
}

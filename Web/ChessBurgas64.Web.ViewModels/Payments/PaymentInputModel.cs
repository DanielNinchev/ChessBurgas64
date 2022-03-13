namespace ChessBurgas64.Web.ViewModels.Payments
{
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class PaymentInputModel : IMapFrom<Payment>
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = GlobalConstants.DateOfPayment)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public string DateOfPayment { get; set; }

        [Required]
        [Display(Name = GlobalConstants.PaidFor)]
        // [StringLength(GlobalConstants.PaidForMinLength, ErrorMessage = "Основанието трябва да бъде с дължина между {1} и {2} символа.", MinimumLength = GlobalConstants.PaidForMaxLength)]
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}

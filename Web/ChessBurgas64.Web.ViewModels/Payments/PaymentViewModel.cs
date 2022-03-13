namespace ChessBurgas64.Web.ViewModels.Payments
{
    using System;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class PaymentViewModel : IMapFrom<Payment>
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateOfPayment { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
    }
}

namespace ChessBurgas64.Data.Models
{
    using System;

    using ChessBurgas64.Data.Common.Models;

    public class Payment : BaseDeletableModel<string>
    {
        public Payment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public decimal Amount { get; set; }

        public DateTime DateOfPayment { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}

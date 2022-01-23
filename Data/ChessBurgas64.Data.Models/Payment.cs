namespace ChessBurgas64.Data.Models
{
    using System;

    using ChessBurgas64.Data.Common.Models;

    public class Payment : BaseDeletableModel<int>
    {
        public decimal Amount { get; set; }

        public DateTime DateOfPayment { get; set; }

        public string Description { get; set; }

        public string MemberId { get; set; }

        public virtual Member Member { get; set; }

        public string TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }
    }
}

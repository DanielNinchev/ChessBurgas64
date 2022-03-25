﻿namespace ChessBurgas64.Data.Models
{
    using ChessBurgas64.Data.Common.Models;

    public class TournamentMember : BaseDeletableModel<int>
    {
        public int TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }

        public string MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}

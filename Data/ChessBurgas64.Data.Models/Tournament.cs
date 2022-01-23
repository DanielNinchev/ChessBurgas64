namespace ChessBurgas64.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class Tournament : BaseDeletableModel<int>
    {
        public Tournament()
        {
            this.Participants = new HashSet<TournamentMember>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Rounds { get; set; }

        public virtual ICollection<TournamentMember> Participants { get; set; }
    }
}

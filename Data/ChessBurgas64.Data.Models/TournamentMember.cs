namespace ChessBurgas64.Data.Models
{
    public class TournamentMember
    {
        public int Id { get; set; }

        public int TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }

        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}

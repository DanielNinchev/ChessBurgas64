namespace ChessBurgas64.Data.Models
{
    public class PuzzleMember
    {
        public int Id { get; set; }

        public int PuzzleId { get; set; }

        public virtual Puzzle Puzzle { get; set; }

        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}

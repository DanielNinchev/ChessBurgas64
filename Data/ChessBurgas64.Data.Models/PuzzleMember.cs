namespace ChessBurgas64.Data.Models
{
    using ChessBurgas64.Data.Common.Models;

    public class PuzzleMember : BaseDeletableModel<int>
    {
        public int PuzzleId { get; set; }

        public virtual Puzzle Puzzle { get; set; }

        public string MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}

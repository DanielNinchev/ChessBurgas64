namespace ChessBurgas64.Data.Models
{
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class PuzzleCategory : BaseDeletableModel<int>
    {
        public PuzzleCategory()
        {
            this.Puzzles = new HashSet<Puzzle>();
        }

        public string Name { get; set; }

        public virtual ICollection<Puzzle> Puzzles { get; set; }
    }
}

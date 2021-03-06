namespace ChessBurgas64.Data.Models
{
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class Puzzle : BaseDeletableModel<int>
    {
        public Puzzle()
        {
            this.Members = new HashSet<PuzzleMember>();
        }

        public string Number { get; set; }

        public string Objective { get; set; }

        public string Solution { get; set; }

        public int Points { get; set; }

        public string Difficulty { get; set; }

        public int CategoryId { get; set; }

        public virtual PuzzleCategory Category { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<PuzzleMember> Members { get; set; }
    }
}

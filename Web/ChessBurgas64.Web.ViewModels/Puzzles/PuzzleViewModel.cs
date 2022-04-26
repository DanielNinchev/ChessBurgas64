namespace ChessBurgas64.Web.ViewModels.Puzzles
{
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class PuzzleViewModel : NumericIdHoldingModel, IMapFrom<Puzzle>
    {
        public int Number { get; set; }

        public string Objective { get; set; }

        public string Solution { get; set; }

        public int Points { get; set; }

        public string Difficulty { get; set; }

        public string CategoryName { get; set; }

        public string ImageImageUrl { get; set; }
    }
}

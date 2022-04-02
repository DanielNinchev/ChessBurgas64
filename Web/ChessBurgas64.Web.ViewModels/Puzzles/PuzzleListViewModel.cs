namespace ChessBurgas64.Web.ViewModels.Puzzles
{
    using System.Collections.Generic;

    public class PuzzleListViewModel : EntityInListViewModel
    {
        public IEnumerable<PuzzleViewModel> Puzzles { get; set; }
    }
}

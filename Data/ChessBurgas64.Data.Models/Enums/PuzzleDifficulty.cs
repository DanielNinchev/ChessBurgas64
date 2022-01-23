namespace ChessBurgas64.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;

    public enum PuzzleDifficulty
    {
        None = 0,

        [Display(Name = GlobalConstants.BeginnerPuzzle)]
        Beginner = 1,

        [Display(Name = GlobalConstants.EasyPuzzle)]
        Low = 2,

        [Display(Name = GlobalConstants.AveragePuzzle)]
        Average = 3,

        [Display(Name = GlobalConstants.HardPuzzles)]
        High = 4,

        [Display(Name = GlobalConstants.ExtremePuzzle)]
        Extreme = 5,
    }
}

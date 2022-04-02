namespace ChessBurgas64.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    using ChessBurgas64.Common;

    public enum PuzzleDifficulty
    {
        [Display(Name = GlobalConstants.BeginnerPuzzle)]
        Начинаеща = 1, // Beginner

        [Display(Name = GlobalConstants.EasyPuzzle)]
        Ниска = 2, // Low

        [Display(Name = GlobalConstants.AveragePuzzle)]
        Средна = 3, // Average

        [Display(Name = GlobalConstants.HardPuzzle)]
        Висока = 4, // High

        [Display(Name = GlobalConstants.ExtremePuzzle)]
        Майсторска = 5, // Extreme (Master)
    }
}

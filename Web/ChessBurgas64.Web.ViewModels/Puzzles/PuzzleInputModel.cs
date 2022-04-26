namespace ChessBurgas64.Web.ViewModels.Puzzles
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;

    public class PuzzleInputModel : CategoryInputModel, IMapFrom<Puzzle>, IHaveCustomMappings
    {
        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [MinLength(GlobalConstants.PuzzleObjectiveMinLength)]
        [MaxLength(GlobalConstants.PuzzleObjectiveMaxLength)]
        public string Objective { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [MinLength(GlobalConstants.PuzzleSolutionMinLength)]
        public string Solution { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public PuzzleDifficulty Difficulty { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public IFormFile PositionImage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var puzzleDifficultyType = typeof(PuzzleDifficulty);

            configuration.CreateMap<Puzzle, PuzzleInputModel>()
                .ForMember(pim => pim.Difficulty, opt => opt
                .MapFrom(p => (PuzzleDifficulty)Enum.Parse(puzzleDifficultyType, p.Difficulty)));
        }
    }
}

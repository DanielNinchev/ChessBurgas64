namespace ChessBurgas64.Web.ViewModels.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PuzzleInputModel : IMapFrom<Puzzle>, IHaveCustomMappings
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        [Required]
        [MinLength(GlobalConstants.PuzzleObjectiveMinLength)]
        [MaxLength(GlobalConstants.PuzzleObjectiveMaxLength)]
        public string Objective { get; set; }

        [Required]
        [MinLength(GlobalConstants.PuzzleSolutionMinLength)]
        public string Solution { get; set; }

        public int Points { get; set; }

        [Required]
        public PuzzleDifficulty Difficulty { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required]
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

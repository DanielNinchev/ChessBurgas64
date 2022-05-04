namespace ChessBurgas64.Web.ViewModels.ClubPlayers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ClubPlayerInputModel : IMapFrom<ClubPlayer>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [StringLength(
            GlobalConstants.ClubPlayersNameMaxLength,
            ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters,
            MinimumLength = GlobalConstants.ClubPlayersNameMinLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [StringLength(
            GlobalConstants.ClubPlayersDescriptionMaxLength,
            ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters,
            MinimumLength = GlobalConstants.ClubPlayersDescriptionMinLength)]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public FideTitle FideTitle { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public IFormFile ProfileImage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var clubPlayerFideTitleType = typeof(FideTitle);

            configuration.CreateMap<ClubPlayer, ClubPlayerInputModel>()
                .ForMember(x => x.FideTitle, opt => opt
                .MapFrom(cp => (FideTitle)Enum.Parse(clubPlayerFideTitleType, cp.FideTitle)));
        }
    }
}

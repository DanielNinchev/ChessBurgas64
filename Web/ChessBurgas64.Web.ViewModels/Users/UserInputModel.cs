namespace ChessBurgas64.Web.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Mapping;

    public class UserInputModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public ClubStatus ClubStatus { get; set; }

        public int FideRating { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public FideTitle FideTitle { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var userFideTitleType = typeof(FideTitle);

            configuration.CreateMap<ApplicationUser, UserInputModel>()
                .ForMember(x => x.FideTitle, opt => opt
                .MapFrom(u => (FideTitle)Enum.Parse(userFideTitleType, u.FideTitle)))
                .ForMember(x => x.ClubStatus, opt => opt.Ignore());
        }
    }
}

namespace ChessBurgas64.Web.ViewModels.NotableMembers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class NotableMemberInputModel : IMapFrom<NotableMember>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [StringLength(
            GlobalConstants.NotableMembersNameMaxLength,
            ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters,
            MinimumLength = GlobalConstants.NotableMembersNameMinLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [Display(Name = GlobalConstants.IsPartOfGovernance)]
        public bool IsPartOfGovernance { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.ThatNumberMustBeGreaterThanOne)]
        public int ListIndex { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public FideTitle FideTitle { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public IFormFile ProfileImage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var notableMemberFideTitleType = typeof(FideTitle);

            configuration.CreateMap<NotableMember, NotableMemberInputModel>()
                .ForMember(x => x.FideTitle, opt => opt
                .MapFrom(nm => (FideTitle)Enum.Parse(notableMemberFideTitleType, nm.FideTitle)));
        }
    }
}

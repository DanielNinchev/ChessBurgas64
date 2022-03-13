namespace ChessBurgas64.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class UserInputModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        [Required]
        public string ClubStatus { get; set; }

        public int FideRating { get; set; }

        [Required]
        public string FideTitle { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserInputModel>()
                .ForMember(x => x.ClubStatus, opt => opt.Ignore());
        }
    }
}

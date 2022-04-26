namespace ChessBurgas64.Web.ViewModels.Trainers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;
    using Microsoft.AspNetCore.Http;

    public class TrainerInputModel : IMapFrom<Trainer>, IHaveCustomMappings
    {
        public string DateOfLastAttendance { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string UserDescription { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public IFormFile ProfilePicture { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<GroupTableViewModel> Groups { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Trainer, TrainerInputModel>()
                .ForMember(x => x.ProfilePicture, opt => opt.Ignore());
        }
    }
}

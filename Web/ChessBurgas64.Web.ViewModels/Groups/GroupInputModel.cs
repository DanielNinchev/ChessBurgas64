namespace ChessBurgas64.Web.ViewModels.Groups
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class GroupInputModel : IMapFrom<Group>, IHaveCustomMappings
    {
        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string TrainingDay { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        [DataType(DataType.Time)]
        public DateTime TrainingHour { get; set; }

        [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
        public string TrainerId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Trainers { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Group, GroupInputModel>()
                .ForMember(gim => gim.TrainingDay, opt => opt.MapFrom(g => g.TrainingDay.ToString()))
                .ForMember(gim => gim.TrainingHour, opt => opt.MapFrom(g => g.TrainingHour));
        }
    }
}

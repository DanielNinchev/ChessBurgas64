namespace ChessBurgas64.Web.ViewModels.Groups
{
    using System;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class GroupTableViewModel : IMapFrom<Group>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TrainingDay { get; set; }

        public DateTime TrainingHour { get; set; }

        public int LowestRating { get; set; }

        public int HighestRating { get; set; }

        public int MembersCount { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Group, GroupTableViewModel>()
        //        .ForMember(gr => gr.MembersCount, grtvm => grtvm.MapFrom(gr => gr.Members.Count));
        //}
    }
}

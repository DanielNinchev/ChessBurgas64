namespace ChessBurgas64.Web.ViewModels.Members
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.GroupMembers;

    public class MemberViewModel : IMapFrom<Member>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public string School { get; set; }

        public DateTime DateOfJoiningTheClub { get; set; }

        public DateTime DateOfLastAttendance { get; set; }

        public string LearnedOpenings { get; set; }

        public int ClubRating { get; set; }

        public int? LastPuzzleLevel { get; set; }

        public DateTime DateOfJoiningCurrentGroup { get; set; }

        public ICollection<GroupMemberViewModel> Groups { get; set; }

        public string UserFirstName { get; set; }

        public string UserMiddleName { get; set; }

        public string UserLastName { get; set; }

        public string FullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Member, MemberViewModel>()
                .ForMember(mvm => mvm.UserFirstName, opt => opt.MapFrom(m => m.User.FirstName))
                .ForMember(mvm => mvm.UserMiddleName, opt => opt.MapFrom(m => m.User.MiddleName))
                .ForMember(mvm => mvm.UserLastName, opt => opt.MapFrom(m => m.User.LastName))
                .ForMember(mvm => mvm.FullName, opt => opt
                .MapFrom(m => $"{m.User.FirstName} {m.User.MiddleName} {m.User.LastName}"));
        }
    }
}

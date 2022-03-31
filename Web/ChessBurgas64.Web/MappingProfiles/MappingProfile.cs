﻿namespace ChessBurgas64.Web.MappingProfiles
{
    using AutoMapper;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.Areas.Identity.Pages.Account;
    using ChessBurgas64.Web.ViewModels.Groups;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using ChessBurgas64.Web.ViewModels.Members;
    using ChessBurgas64.Web.ViewModels.Payments;
    using ChessBurgas64.Web.ViewModels.Trainers;
    using ChessBurgas64.Web.ViewModels.ViewComponents;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Announcement, CreateAnnouncementInputModel>().ReverseMap();
            this.CreateMap<Group, GroupInputModel>().ReverseMap();
            this.CreateMap<Lesson, LessonInputModel>().ReverseMap();
            this.CreateMap<Member, MemberInputModel>().ReverseMap();
            this.CreateMap<Member, MemberProfileModel>().ReverseMap();
            this.CreateMap<Payment, PaymentInputModel>().ReverseMap();
            this.CreateMap<Trainer, TrainerInputModel>().ReverseMap();
            this.CreateMap<ApplicationUser, RegisterModel.InputModel>().ReverseMap()
                .ForMember(apu => apu.UserName, rm => rm.MapFrom(apu => apu.Email));
        }
    }
}

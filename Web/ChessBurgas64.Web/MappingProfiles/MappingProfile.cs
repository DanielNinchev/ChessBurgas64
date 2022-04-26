namespace ChessBurgas64.Web.MappingProfiles
{
    using AutoMapper;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.Areas.Identity.Pages.Account;
    using ChessBurgas64.Web.ViewModels.Announcements;
    using ChessBurgas64.Web.ViewModels.Groups;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using ChessBurgas64.Web.ViewModels.Members;
    using ChessBurgas64.Web.ViewModels.Payments;
    using ChessBurgas64.Web.ViewModels.Puzzles;
    using ChessBurgas64.Web.ViewModels.Trainers;
    using ChessBurgas64.Web.ViewModels.Users;
    using ChessBurgas64.Web.ViewModels.Videos;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Announcement, AnnouncementInputModel>().ReverseMap();
            this.CreateMap<Group, GroupInputModel>().ReverseMap();
            this.CreateMap<Lesson, LessonInputModel>().ReverseMap();
            this.CreateMap<Member, MemberInputModel>().ReverseMap();
            this.CreateMap<Member, MemberProfileModel>().ReverseMap();
            this.CreateMap<Payment, PaymentInputModel>().ReverseMap();
            this.CreateMap<Puzzle, PuzzleInputModel>().ReverseMap();
            this.CreateMap<Trainer, TrainerInputModel>().ReverseMap();
            this.CreateMap<Video, VideoInputModel>().ReverseMap();
            this.CreateMap<ApplicationUser, UserInputModel>().ReverseMap();
            this.CreateMap<ApplicationUser, Register.InputModel>().ReverseMap()
                .ForMember(apu => apu.UserName, rm => rm.MapFrom(apu => apu.Email));
        }
    }
}

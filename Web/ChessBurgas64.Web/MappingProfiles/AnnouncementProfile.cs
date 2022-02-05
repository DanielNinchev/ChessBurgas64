namespace ChessBurgas64.Web.MappingProfiles
{
    using AutoMapper;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.ViewComponents;

    public class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            this.CreateMap<Announcement, CreateAnnouncementInputModel>().ReverseMap();
        }
    }
}

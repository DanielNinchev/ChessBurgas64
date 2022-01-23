namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using System;
    using System.Linq;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class AnnouncementInCardViewModel : IMapFrom<Announcement>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Announcement, AnnouncementInCardViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt
                .MapFrom(a => a.Images.FirstOrDefault().RemoteImageUrl ??
                GlobalConstants.AnnouncementImagesPath + a.Images.FirstOrDefault().Id + a.Images.FirstOrDefault().Extension));
        }
    }
}

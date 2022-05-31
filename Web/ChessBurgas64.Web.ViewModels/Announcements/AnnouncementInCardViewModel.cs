namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class AnnouncementInCardViewModel : IdHoldingModel, IMapFrom<Announcement>
    {
        public string MainImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        [NotMapped]
        public string AuthorName => $"{this.AuthorFirstName} {this.AuthorLastName}";
    }
}

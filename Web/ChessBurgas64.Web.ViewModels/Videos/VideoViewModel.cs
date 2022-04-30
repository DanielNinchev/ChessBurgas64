namespace ChessBurgas64.Web.ViewModels.Videos
{
    using System;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Trainers;

    public class VideoViewModel : IdHoldingModel, IMapFrom<Video>
    {
        public string Title { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public TrainerViewModel Trainer { get; set; }
    }
}

namespace ChessBurgas64.Web.ViewModels.Videos
{
    using System.Collections.Generic;

    public class VideosListViewModel : EntityInListViewModel
    {
        public IEnumerable<VideoViewModel> Videos { get; set; }
    }
}

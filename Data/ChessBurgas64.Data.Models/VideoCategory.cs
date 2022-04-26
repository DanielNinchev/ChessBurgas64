namespace ChessBurgas64.Data.Models
{
    using System.Collections.Generic;

    using ChessBurgas64.Data.Common.Models;

    public class VideoCategory : BaseDeletableModel<int>
    {
        public VideoCategory()
        {
            this.Videos = new HashSet<Video>();
        }

        public string Name { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
}

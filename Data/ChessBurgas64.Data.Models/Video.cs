namespace ChessBurgas64.Data.Models
{
    using ChessBurgas64.Data.Common.Models;

    public class Video : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int CategoryId { get; set; }

        public virtual VideoCategory Category { get; set; }

        public string TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }
    }
}

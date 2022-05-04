namespace ChessBurgas64.Data.Models
{
    using ChessBurgas64.Data.Common.Models;

    public class ClubPlayer : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string FideTitle { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}

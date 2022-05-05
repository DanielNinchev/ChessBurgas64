namespace ChessBurgas64.Data.Models
{
    using ChessBurgas64.Data.Common.Models;

    public class NotableMember : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string FideTitle { get; set; }

        public bool IsPartOfGovernance { get; set; }

        public int ListIndex { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}

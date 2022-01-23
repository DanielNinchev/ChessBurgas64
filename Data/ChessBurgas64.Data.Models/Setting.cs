namespace ChessBurgas64.Data.Models
{
    using ChessBurgas64.Data.Common.Models;

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}

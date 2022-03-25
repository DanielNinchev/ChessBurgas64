namespace ChessBurgas64.Data.Models
{
    using ChessBurgas64.Data.Common.Models;

    public class GroupMember : BaseDeletableModel<int>
    {
        public string GroupId { get; set; }

        public virtual Group Group { get; set; }

        public string MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}

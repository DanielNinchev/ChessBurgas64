namespace ChessBurgas64.Web.ViewModels.GroupMembers
{
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;
    using ChessBurgas64.Web.ViewModels.Members;

    public class GroupMemberViewModel : IMapFrom<GroupMember>
    {
        public int Id { get; set; }

        public string GroupId { get; set; }

        public GroupViewModel Group { get; set; }

        public string MemberId { get; set; }

        public MemberViewModel Member { get; set; }

        [NotMapped]
        public bool Selected { get; set; }
    }
}

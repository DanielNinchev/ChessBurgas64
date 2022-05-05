namespace ChessBurgas64.Web.ViewModels.NotableMembers
{
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class NotableMemberViewModel : IdHoldingModel, IMapFrom<NotableMember>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string FideTitle { get; set; }

        public bool IsPartOfGovernance { get; set; }

        public string ImageImageUrl { get; set; }
    }
}

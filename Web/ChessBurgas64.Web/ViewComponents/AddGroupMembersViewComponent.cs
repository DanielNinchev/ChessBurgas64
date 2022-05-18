namespace ChessBurgas64.Web.ViewComponents
{
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class AddGroupMembersViewComponent : ViewComponent
    {
        private readonly IMembersService membersService;

        public AddGroupMembersViewComponent(IMembersService membersService)
        {
            this.membersService = membersService;
        }

        public IViewComponentResult Invoke(string title)
        {
            var groupId = this.HttpContext.Session.GetString("groupId");
            var members = this.membersService.GetNecessaryMembersInSelectList(groupId);
            var viewModel = new GroupMemberInputModel
            {
                Members = members,
            };

            return this.View(viewModel);
        }
    }
}

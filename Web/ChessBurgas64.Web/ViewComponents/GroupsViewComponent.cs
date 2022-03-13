namespace ChessBurgas64.Web.ViewComponents
{
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using Microsoft.AspNetCore.Mvc;

    public class GroupsViewComponent : ViewComponent
    {
        private readonly IGroupsService groupsService;

        public GroupsViewComponent(IGroupsService groupsService)
        {
            this.groupsService = groupsService;
        }

        public IViewComponentResult Invoke(string title)
        {
            var groups = this.groupsService.GetAllGroups();
            var viewModel = new LessonInputModel
            {
                GroupName = title,
                Groups = groups,
            };

            return this.View(viewModel);
        }
    }
}

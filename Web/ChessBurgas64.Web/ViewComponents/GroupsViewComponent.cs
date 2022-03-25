namespace ChessBurgas64.Web.ViewComponents
{
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class GroupsViewComponent : ViewComponent
    {
        private readonly IGroupsService groupsService;

        public GroupsViewComponent(IGroupsService groupsService)
        {
            this.groupsService = groupsService;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.HttpContext.Session.GetString("userId");
            var groups = this.groupsService.GetAllTrainerGroups(userId);
            var viewModel = new LessonInputModel
            {
                Groups = groups,
            };

            return this.View(viewModel);
        }
    }
}

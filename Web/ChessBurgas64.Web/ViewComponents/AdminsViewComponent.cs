namespace ChessBurgas64.Web.ViewComponents
{
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Announcements;
    using ChessBurgas64.Web.ViewModels.Groups;
    using Microsoft.AspNetCore.Mvc;

    public class AdminsViewComponent : ViewComponent
    {
        private readonly IUsersService usersService;

        public AdminsViewComponent(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IViewComponentResult Invoke(string title)
        {
            var admins = this.usersService.GetAllAdminsInSelectList();
            var viewModel = new AnnouncementInputModel
            {
                Authors = admins,
            };

            return this.View(viewModel);
        }
    }
}
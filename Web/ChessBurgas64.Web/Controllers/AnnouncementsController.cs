namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Announcements;
    using ChessBurgas64.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AnnouncementsController : Controller
    {
        private readonly IAnnouncementsService announcementsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public AnnouncementsController(
            IAnnouncementsService announcementsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.announcementsService = announcementsService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.announcementsService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}{GlobalConstants.AnnouncementImagesPath}");
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            var viewModel = new AnnouncementsListViewModel
            {
                ItemsPerPage = GlobalConstants.AnnouncementsPerPage,
                PageNumber = id,
                AnnouncementsCount = this.announcementsService.GetCount(),
                Announcements = this.announcementsService.GetAll<AnnouncementInCardViewModel>(id, GlobalConstants.AnnouncementsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var announcement = this.announcementsService.GetById<SingleAnnouncementViewModel>(id);

            return this.View(announcement);
        }
    }
}

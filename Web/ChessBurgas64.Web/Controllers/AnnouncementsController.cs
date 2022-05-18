namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels;
    using ChessBurgas64.Web.ViewModels.Announcements;
    using ChessBurgas64.Web.ViewModels.Categories;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AnnouncementsController : Controller
    {
        private readonly IAnnouncementsService announcementsService;
        private readonly ICategoriesService categoriesService;
        private readonly IHtmlSanitizer sanitizer;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public AnnouncementsController(
            IAnnouncementsService announcementsService,
            ICategoriesService categoriesService,
            IHtmlSanitizer sanitizer,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.announcementsService = announcementsService;
            this.categoriesService = categoriesService;
            this.sanitizer = sanitizer;
            this.userManager = userManager;
            this.environment = environment;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new AnnouncementsListViewModel
            {
                ItemsPerPage = GlobalConstants.AnnouncementsPerPage,
                PageNumber = id,
                Count = await this.announcementsService.GetCountAsync(),
                Announcements = await this.announcementsService.GetAllAsync<AnnouncementInCardViewModel>(id, GlobalConstants.AnnouncementsPerPage),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int id)
        {
            var announcement = await this.announcementsService.GetByIdAsync<SingleAnnouncementViewModel>(id);
            announcement.Text = this.sanitizer.Sanitize(announcement.Text);
            return this.View(announcement);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(AnnouncementInputModel input)
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

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.announcementsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            var inputModel = await this.announcementsService.GetByIdAsync<AnnouncementInputModel>(id);
            inputModel.Categories = this.categoriesService.GetAnnouncementCategoriesInSelectList();
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, AnnouncementInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAnnouncementCategoriesInSelectList();
                return this.View(input);
            }

            await this.announcementsService.UpdateAsync(id, input, $"{this.environment.WebRootPath}{GlobalConstants.AnnouncementImagesPath}");
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        public async Task<IActionResult> Search()
        {
            var viewModel = new SearchViewModel
            {
                Categories = await this.categoriesService.GetAllAnnouncementCategoriesAsync<AnnouncementCategoryViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Searched(SearchInputModel input, IDictionary<string, string> parms, int id = 1)
        {
            try
            {
                // If we are coming from _PagingPartial, the input parameters will be null, so we will have to initialize them
                if (input.Categories == null)
                {
                    this.categoriesService.InitializeSearchedParameters(input, parms);
                }

                var viewModel = new AnnouncementsListViewModel
                {
                    IsSearched = true,
                    ItemsPerPage = GlobalConstants.PuzzlesPerPage,
                    PageNumber = id,
                    Announcements = await this.announcementsService.GetSearchedAsync<AnnouncementInCardViewModel>(input.Categories, input.SearchText),
                };

                if (viewModel.Announcements != null)
                {
                    viewModel.Count = viewModel.Announcements.Count;
                    viewModel.Announcements = viewModel.Announcements
                        .ToList()
                        .OrderByDescending(x => x.Date)
                        .Skip((viewModel.PageNumber - 1) * viewModel.ItemsPerPage)
                        .Take(viewModel.ItemsPerPage)
                        .ToList();
                }

                viewModel.Search = new SearchViewModel()
                {
                    Categories = await this.categoriesService.GetCategoriesByIdsAsync<AnnouncementCategoryViewModel>(input.Categories, nameof(AnnouncementsController)),
                    SearchText = input.SearchText,
                };

                return this.View(viewModel);
            }
            catch (Exception e)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }
    }
}

namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels;
    using ChessBurgas64.Web.ViewModels.Categories;
    using ChessBurgas64.Web.ViewModels.Videos;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class VideosController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IVideosService videosService;

        public VideosController(
            ICategoriesService categoriesService,
            IVideosService videosService)
        {
            this.categoriesService = categoriesService;
            this.videosService = videosService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new VideosListViewModel
            {
                ItemsPerPage = GlobalConstants.VideosPerPage,
                PageNumber = id,
                Count = await this.videosService.GetCountAsync(),
                Videos = await this.videosService.GetAllAsync<VideoViewModel>(id, GlobalConstants.VideosPerPage),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int id)
        {
            var video = await this.videosService.GetByIdAsync<VideoViewModel>(id);

            return this.View(video);
        }

        [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
        public async Task<IActionResult> Create(VideoInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.videosService.CreateAsync(input);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            return this.Redirect(nameof(this.All));
        }

        [HttpPost]
        [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.videosService.DeleteAsync(id);
                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
        public async Task<IActionResult> Edit(int id)
        {
            var inputModel = await this.videosService.GetByIdAsync<VideoInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
        public async Task<IActionResult> Edit(int id, VideoInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.videosService.UpdateAsync(id, input);
                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        public async Task<IActionResult> Search()
        {
            var viewModel = new SearchViewModel
            {
                Categories = await this.categoriesService.GetAllVideoCategoriesAsync<VideoCategoryViewModel>(),
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

                var viewModel = new VideosListViewModel
                {
                    IsSearched = true,
                    ItemsPerPage = GlobalConstants.PuzzlesPerPage,
                    PageNumber = id,
                    Videos = await this.videosService.GetSearchedAsync<VideoViewModel>(input.Categories, input.SearchText),
                };

                if (viewModel.Videos != null)
                {
                    viewModel.Count = viewModel.Videos.Count();
                    viewModel.Videos = viewModel.Videos
                        .ToList()
                        .OrderByDescending(x => x.CreatedOn)
                        .Skip((viewModel.PageNumber - 1) * viewModel.ItemsPerPage)
                        .Take(viewModel.ItemsPerPage)
                        .ToList();
                }

                viewModel.Search = new SearchViewModel()
                {
                    Categories = await this.categoriesService.GetCategoriesByIdsAsync<VideoCategoryViewModel>(input.Categories, nameof(VideosController)),
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

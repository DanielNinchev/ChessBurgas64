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

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new VideosListViewModel
            {
                ItemsPerPage = GlobalConstants.VideosPerPage,
                PageNumber = id,
                Count = this.videosService.GetCount(),
                Videos = this.videosService.GetAll<VideoViewModel>(id, GlobalConstants.VideosPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var video = this.videosService.GetById<VideoViewModel>(id);

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
            await this.videosService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
        public IActionResult Edit(int id)
        {
            var inputModel = this.videosService.GetById<VideoInputModel>(id);

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

            await this.videosService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Search()
        {
            var viewModel = new SearchViewModel
            {
                Categories = this.categoriesService.GetAllVideoCategories<VideoCategoryViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Searched(SearchInputModel input, IDictionary<string, string> parms, int id = 1)
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
                    Videos = this.videosService.GetSearched<VideoViewModel>(input.Categories, input.SearchText),
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
                    Categories = this.categoriesService.GetCategoriesByIds<VideoCategoryViewModel>(input.Categories, nameof(VideosController)),
                    SearchText = input.SearchText,
                };

                return this.View(viewModel);
            }
            catch (Exception e)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }
    }
}

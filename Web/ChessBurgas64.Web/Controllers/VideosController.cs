namespace ChessBurgas64.Web.Controllers
{
    using System;
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
        public IActionResult Searched(SearchInputModel input, int id = 1)
        {
            var viewModel = new VideosListViewModel
            {
                ItemsPerPage = GlobalConstants.VideosPerPage,
                PageNumber = id,
                Count = this.videosService.GetCount(),
                Videos = this.videosService.GetSearched<VideoViewModel>(
                    id, GlobalConstants.VideosPerPage, input.Categories, input.SearchText),
            };

            return this.View(viewModel);
        }
    }
}

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
    using ChessBurgas64.Web.ViewModels.Puzzles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class PuzzlesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IImagesService imagesService;
        private readonly IPuzzlesService puzzlesService;
        private readonly IWebHostEnvironment environment;

        public PuzzlesController(
            ICategoriesService categoriesService,
            IImagesService imagesService,
            IPuzzlesService puzzlesService,
            IWebHostEnvironment environment)
        {
            this.categoriesService = categoriesService;
            this.imagesService = imagesService;
            this.puzzlesService = puzzlesService;
            this.environment = environment;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new PuzzleListViewModel
            {
                IsSearched = false,
                ItemsPerPage = GlobalConstants.PuzzlesPerPage,
                PageNumber = id,
                Count = await this.puzzlesService.GetCountAsync(),
                Puzzles = await this.puzzlesService.GetAllAsync<PuzzleViewModel>(id, GlobalConstants.PuzzlesPerPage),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
        public async Task<IActionResult> Create(PuzzleInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var webRootImagePath = $"{this.environment.WebRootPath}{GlobalConstants.PuzzleImagesPath}";
                var puzzle = await this.puzzlesService.CreateAsync(input, webRootImagePath);
                await this.imagesService.InitializePuzzleImageAsync(input.PositionImage, puzzle, webRootImagePath);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var puzzle = await this.puzzlesService.GetByIdAsync(id);
                await this.puzzlesService.DeleteAsync(id);
                await this.imagesService.DeleteAsync(puzzle.ImageId);
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
            var inputModel = await this.puzzlesService.GetByIdAsync<PuzzleInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
        public async Task<IActionResult> Edit(int id, PuzzleInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var webRootImagePath = $"{this.environment.WebRootPath}{GlobalConstants.PuzzleImagesPath}";
                var puzzle = await this.puzzlesService.UpdateAsync(id, input);
                await this.imagesService.InitializePuzzleImageAsync(input.PositionImage, puzzle, webRootImagePath);
                return this.RedirectToAction(nameof(this.All));
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }
        }

        public async Task<IActionResult> Search()
        {
            var viewModel = new SearchViewModel
            {
                Categories = await this.categoriesService.GetAllPuzzleCategoriesAsync<PuzzleCategoryViewModel>(),
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

                var viewModel = new PuzzleListViewModel
                {
                    IsSearched = true,
                    ItemsPerPage = GlobalConstants.PuzzlesPerPage,
                    PageNumber = id,
                    Puzzles = await this.puzzlesService.GetSearchedAsync<PuzzleViewModel>(input.Categories, input.SearchText),
                };

                if (viewModel.Puzzles != null)
                {
                    viewModel.Count = viewModel.Puzzles.Count;
                    viewModel.Puzzles = viewModel.Puzzles
                        .ToList()
                        .OrderBy(x => x.Number)
                        .Skip((viewModel.PageNumber - 1) * viewModel.ItemsPerPage)
                        .Take(viewModel.ItemsPerPage)
                        .ToList();
                }

                viewModel.Search = new SearchViewModel()
                {
                    Categories = await this.categoriesService.GetCategoriesByIdsAsync<PuzzleCategoryViewModel>(input.Categories, nameof(PuzzlesController)),
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

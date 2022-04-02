namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Puzzles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PuzzlesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IImagesService imagesService;
        private readonly IPuzzlesService puzzlesService;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<ApplicationUser> userManager;

        public PuzzlesController(
            ICategoriesService categoriesService,
            IImagesService imagesService,
            IPuzzlesService puzzlesService,
            IWebHostEnvironment environment,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.imagesService = imagesService;
            this.puzzlesService = puzzlesService;
            this.environment = environment;
            this.userManager = userManager;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new PuzzleListViewModel
            {
                ItemsPerPage = GlobalConstants.PuzzlesPerPage,
                PageNumber = id,
                Count = this.puzzlesService.GetCount(),
                Puzzles = this.puzzlesService.GetAll<PuzzleViewModel>(id, GlobalConstants.PuzzlesPerPage),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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
                await this.imagesService.InitializePuzzleImage(input.PositionImage, puzzle, webRootImagePath);
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
            try
            {
                var puzzle = this.puzzlesService.GetById(id);
                await this.puzzlesService.DeleteAsync(id);
                await this.imagesService.DeleteAsync(puzzle.ImageId);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.puzzlesService.GetById<PuzzleInputModel>(id);

            return this.View(inputModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PuzzleInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var webRootImagePath = $"{this.environment.WebRootPath}{GlobalConstants.PuzzleImagesPath}";
            var puzzle = await this.puzzlesService.UpdateAsync(id, input);
            await this.imagesService.InitializePuzzleImage(input.PositionImage, puzzle, webRootImagePath);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}

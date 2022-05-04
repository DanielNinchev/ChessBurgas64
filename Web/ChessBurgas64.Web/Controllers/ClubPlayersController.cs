namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.ClubPlayers;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class ClubPlayersController : Controller
    {
        private readonly IClubPlayersService clubPlayersService;
        private readonly IImagesService imagesService;
        private readonly IHtmlSanitizer sanitizer;
        private readonly IWebHostEnvironment environment;

        public ClubPlayersController(
            IClubPlayersService clubPlayersService,
            IImagesService imagesService,
            IHtmlSanitizer sanitizer,
            IWebHostEnvironment environment)
        {
            this.clubPlayersService = clubPlayersService;
            this.imagesService = imagesService;
            this.sanitizer = sanitizer;
            this.environment = environment;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new ClubPlayersListViewModel
            {
                ItemsPerPage = GlobalConstants.ClubPlayersPerPage,
                PageNumber = id,
                Count = this.clubPlayersService.GetCount(),
                ClubPlayers = this.clubPlayersService.GetAll<ClubPlayerViewModel>(id, GlobalConstants.ClubPlayersPerPage),
            };

            foreach (var clubPlayer in viewModel.ClubPlayers)
            {
                clubPlayer.Description = this.sanitizer.Sanitize(clubPlayer.Description);
            }

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(ClubPlayerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var webRootImagePath = $"{this.environment.WebRootPath}{GlobalConstants.ClubPlayerImagesPath}";
                var clubPlayer = await this.clubPlayersService.CreateAsync(input, webRootImagePath);

                await this.imagesService.InitializeClubPlayerImage(input.ProfileImage, clubPlayer, webRootImagePath);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            return this.Redirect(nameof(this.All));
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var clubPlayer = this.clubPlayersService.GetById(id);
                await this.clubPlayersService.DeleteAsync(id);
                await this.imagesService.DeleteAsync(clubPlayer.ImageId);
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
            var inputModel = this.clubPlayersService.GetById<ClubPlayerInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, ClubPlayerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var webRootImagePath = $"{this.environment.WebRootPath}{GlobalConstants.ClubPlayerImagesPath}";
            var clubPlayer = await this.clubPlayersService.UpdateAsync(id, input);
            await this.imagesService.InitializeClubPlayerImage(input.ProfileImage, clubPlayer, webRootImagePath);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}

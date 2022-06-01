namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.NotableMembers;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class NotableMembersController : Controller
    {
        private readonly INotableMembersService notableMembersService;
        private readonly IImagesService imagesService;
        private readonly IHtmlSanitizer sanitizer;
        private readonly IWebHostEnvironment environment;

        public NotableMembersController(
            INotableMembersService notableMembersService,
            IImagesService imagesService,
            IHtmlSanitizer sanitizer,
            IWebHostEnvironment environment)
        {
            this.notableMembersService = notableMembersService;
            this.imagesService = imagesService;
            this.sanitizer = sanitizer;
            this.environment = environment;
        }

        public async Task<IActionResult> Competitors()
        {
            try
            {
                var viewModel = new NotableMembersListViewModel
                {
                    NotableMembers = await this.notableMembersService.GetAllPlayersAsync<NotableMemberViewModel>(),
                };

                foreach (var notableMember in viewModel.NotableMembers)
                {
                    notableMember.Description = this.sanitizer.Sanitize(notableMember.Description);
                }

                return this.View(viewModel);
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(NotableMemberInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var webRootImagePath = $"{this.environment.WebRootPath}{GlobalConstants.NotableMembersImagesPath}";
                var notableMember = await this.notableMembersService.CreateAsync(input, webRootImagePath);

                await this.imagesService.InitializeNotableMemberImageAsync(input.ProfileImage, notableMember, webRootImagePath);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            return this.Redirect(nameof(this.Governance));
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            string actionName = nameof(this.Competitors);

            try
            {
                var notableMember = await this.notableMembersService.GetByIdAsync(id);
                actionName = notableMember.IsPartOfGovernance ? nameof(this.Governance) : nameof(this.Competitors);

                await this.notableMembersService.DeleteAsync(id);
                await this.imagesService.DeleteAsync(notableMember.ImageId);
            }
            catch (Exception e)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }

            return this.RedirectToAction(actionName);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            var inputModel = await this.notableMembersService.GetByIdAsync<NotableMemberInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, NotableMemberInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var webRootImagePath = $"{this.environment.WebRootPath}{GlobalConstants.NotableMembersImagesPath}";
                var notableMember = await this.notableMembersService.UpdateAsync(id, input);
                await this.imagesService.InitializeNotableMemberImageAsync(input.ProfileImage, notableMember, webRootImagePath);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.Governance));
        }

        public async Task<IActionResult> Governance()
        {
            try
            {
                var viewModel = new NotableMembersListViewModel
                {
                    NotableMembers = await this.notableMembersService.GetAllInGovernanceAsync<NotableMemberViewModel>(),
                };

                foreach (var notableMember in viewModel.NotableMembers)
                {
                    notableMember.Description = this.sanitizer.Sanitize(notableMember.Description);
                }

                return this.View(viewModel);
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }
    }
}

namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class DeletePersonalData : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUsersService usersService;
        private readonly ILogger<DeletePersonalData> logger;

        public DeletePersonalData(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUsersService usersService,
            ILogger<DeletePersonalData> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.usersService = usersService;
            this.logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.RequirePassword = await this.userManager.HasPasswordAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.RequirePassword = await this.userManager.HasPasswordAsync(user);
            if (this.RequirePassword)
            {
                if (!await this.userManager.CheckPasswordAsync(user, this.Input.Password))
                {
                    this.ModelState.AddModelError(string.Empty, ErrorMessages.InvalidPassword);
                    return this.Page();
                }
            }

            try
            {
                await this.usersService.DeleteAsync(user);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

            var userId = await this.userManager.GetUserIdAsync(user);

            //if (!result.Succeeded)
            //{
            //    throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            //}

            await this.signInManager.SignOutAsync();

            this.logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return this.Redirect("~/");
        }
    }
}

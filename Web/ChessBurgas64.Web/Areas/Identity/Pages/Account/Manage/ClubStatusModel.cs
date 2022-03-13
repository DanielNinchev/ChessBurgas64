namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class ClubStatusModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;

        public ClubStatusModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public ClubStatus ClubStatus { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.ValidateUser(user);

            if (this.ClubStatus != user.ClubStatus)
            {
                await this.emailSender.SendEmailAsync(
                    GlobalConstants.AdminEmail,
                    GlobalConstants.StatusValidationTopic,
                    $"{user.FirstName}{user.MiddleName}{user.LastName}{GlobalConstants.StatusValidationMsg}{this.ClubStatus}");

                this.StatusMessage = GlobalConstants.ResendEmailConfirmationInstructions;
                return this.RedirectToPage();
            }

            await this.userManager.UpdateAsync(user);
            await this.signInManager.RefreshSignInAsync(user);

            this.StatusMessage = GlobalConstants.ClubStatusConfirmed;

            return this.RedirectToPage();
        }

        public async Task<IActionResult> ValidateUser(ApplicationUser user)
        {
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                this.ClubStatus = user.ClubStatus;
                return this.Page();
            }

            return null;
        }
    }
}

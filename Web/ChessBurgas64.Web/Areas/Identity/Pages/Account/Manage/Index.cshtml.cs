namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using AspNetCore.ReCaptcha;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;

    [ValidateReCaptcha(ErrorMessage = ErrorMessages.InvalidCaptcha)]
    public partial class Index : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;

        public Index(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        [Display(Name = GlobalConstants.Email)]
        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmail)]
            [Display(Name = GlobalConstants.NewEmail)]
            public string NewEmail { get; set; }

            [DataType(DataType.Password, ErrorMessage = ErrorMessages.InvalidPassword)]
            [Display(Name = GlobalConstants.CurrentPassword)]
            public string OldPassword { get; set; }

            [StringLength(GlobalConstants.PasswordMaxLength, ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters, MinimumLength = GlobalConstants.PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.NewPassword)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.ConfirmNewPassword)]
            [Compare(nameof(NewPassword), ErrorMessage = ErrorMessages.PasswordsDoNotMatch)]
            public string ConfirmPassword { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var email = await this.userManager.GetEmailAsync(user);

            this.Username = userName;
            this.Email = email;

            this.Input = new InputModel
            {
                NewEmail = email,
            };

            this.IsEmailConfirmed = await this.userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var hasPassword = await this.userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return this.RedirectToPage("./SetPassword");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.ValidateModel(user);

            if (this.Input.OldPassword != null && this.Input.NewPassword != null)
            {
                var changePasswordResult = await this.userManager.ChangePasswordAsync(user, this.Input.OldPassword, this.Input.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                    }

                    await this.LoadAsync(user);

                    return this.Page();
                }

                await this.userManager.UpdateAsync(user);
                await this.signInManager.RefreshSignInAsync(user);

                this.StatusMessage = GlobalConstants.ProfileDataUpdatedMsg;
            }

            await this.ChangeEmailAsync(user);

            return this.RedirectToPage();
        }

        public async Task<IActionResult> ChangeEmailAsync(ApplicationUser user)
        {
            var email = await this.userManager.GetEmailAsync(user);

            if (this.Input.NewEmail != email && this.Input.NewEmail != null)
            {
                var userId = await this.userManager.GetUserIdAsync(user);
                var code = await this.userManager.GenerateChangeEmailTokenAsync(user, this.Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = this.Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId, email = this.Input.NewEmail, code },
                    protocol: this.Request.Scheme);
                await this.emailSender.SendEmailAsync(
                    this.Input.NewEmail,
                    GlobalConstants.EmailConfirmationTopic,
                    $"{GlobalConstants.EmailConfirmationMsg} <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>{GlobalConstants.EmailConfirmationTopic}</a>.");

                if (this.StatusMessage == GlobalConstants.ProfileDataUpdatedMsg)
                {
                    this.StatusMessage = $"{GlobalConstants.ProfileDataUpdatedMsg} {GlobalConstants.ResendEmailConfirmationInstructions}";
                }
                else
                {
                    this.StatusMessage = GlobalConstants.ResendEmailConfirmationInstructions;
                }
            }

            return this.RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.ValidateModel(user);

            var userId = await this.userManager.GetUserIdAsync(user);
            var email = await this.userManager.GetEmailAsync(user);
            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = this.Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: this.Request.Scheme);
            await this.emailSender.SendEmailAsync(
                email,
                GlobalConstants.EmailConfirmationTopic,
                $"{GlobalConstants.EmailConfirmationMsg} <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>{GlobalConstants.EmailConfirmationTopic}</a>.");

            this.StatusMessage = GlobalConstants.ResendEmailConfirmationInstructions;
            return this.RedirectToPage();
        }

        public async Task<IActionResult> ValidateModel(ApplicationUser user)
        {
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                this.StatusMessage = ErrorMessages.InvalidInputData;
                await this.LoadAsync(user);
                return this.Page();
            }

            return null;
        }
    }
}

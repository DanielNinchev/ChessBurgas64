namespace ChessBurgas64.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.FirstName)]
            public string FirstName { get; set; }

            [Required]
            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.MiddleName)]
            public string MiddleName { get; set; }

            [Required]
            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.LastName)]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = GlobalConstants.Email)]
            public string Email { get; set; }

            [Required]
            [StringLength(GlobalConstants.PasswordMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = GlobalConstants.PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.RepeatPass)]
            [Compare("Password", ErrorMessage = ErrorMessages.PasswordsDoNotMatch)]
            public string ConfirmPassword { get; set; }

            [Required]
            [Phone]
            [Display(Name = GlobalConstants.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = GlobalConstants.BirthDate)]
            public DateTime BirthDate { get; set; }

            [Required]
            [Display(Name = GlobalConstants.Gender)]
            public Gender Gender { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = this.Input.FirstName,
                    MiddleName = this.Input.MiddleName,
                    LastName = this.Input.LastName,
                    UserName = this.Input.Email,
                    Email = this.Input.Email,
                    BirthDate = this.Input.BirthDate,
                    Gender = this.Input.Gender,
                };
                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        this.Input.Email,
                        GlobalConstants.RegistrationConfirmationTopic,
                        $"{GlobalConstants.RegistrationConfirmationMsg} <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>{GlobalConstants.RegistrationConfirmationTopic}</a>.");

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}

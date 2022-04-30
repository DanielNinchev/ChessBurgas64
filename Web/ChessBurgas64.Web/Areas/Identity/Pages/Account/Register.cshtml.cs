namespace ChessBurgas64.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using AspNetCore.ReCaptcha;
    using AutoMapper;
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
    [ValidateReCaptcha]
    public class Register : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<Register> logger;
        private readonly IEmailSender emailSender;
        private readonly IMapper mapper;

        public Register(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<Register> logger,
            IEmailSender emailSender,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.mapper = mapper;
            this.StatusMessage = null;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters, MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.FirstName)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters, MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.MiddleName)]
            public string MiddleName { get; set; }

            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters, MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.LastName)]
            public string LastName { get; set; }

            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [EmailAddress]
            [Display(Name = GlobalConstants.Email)]
            public string Email { get; set; }

            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [StringLength(GlobalConstants.PasswordMaxLength, ErrorMessage = ErrorMessages.ThatFieldRequiresNumberOfCharacters, MinimumLength = GlobalConstants.PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.RepeatPass)]
            [Compare("Password", ErrorMessage = ErrorMessages.PasswordsDoNotMatch)]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [Phone]
            [Display(Name = GlobalConstants.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [Display(Name = GlobalConstants.BirthDate)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
            [DataType(DataType.Date)]
            public DateTime BirthDate { get; set; }

            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [Display(Name = GlobalConstants.Gender)]
            public string Gender { get; set; }

            [Required(ErrorMessage = ErrorMessages.ThatFieldIsRequired)]
            [NotMapped]
            [Display(Name = GlobalConstants.ClubStatus)]
            public string ClubStatus { get; set; }
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
                var user = this.mapper.Map<ApplicationUser>(this.Input);
                user.ClubStatus = ClubStatus.Изчакващ.ToString(); // Pending
                user.FideTitle = FideTitle.Няма.ToString(); // None

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

                    await this.emailSender.SendEmailAsync(
                        GlobalConstants.AdminEmail,
                        GlobalConstants.StatusValidationTopic,
                        $"{this.Input.FirstName} {this.Input.MiddleName} {this.Input.LastName} {GlobalConstants.StatusValidationMsg}{this.Input.ClubStatus}");

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
            this.StatusMessage = ErrorMessages.InvalidCaptcha;
            return this.Page();
        }
    }
}

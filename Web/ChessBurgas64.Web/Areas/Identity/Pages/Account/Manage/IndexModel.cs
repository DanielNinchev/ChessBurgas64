﻿namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        private readonly ILogger<MemberDataModel> logger;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<MemberDataModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.logger = logger;
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
            [EmailAddress]
            [Display(Name = GlobalConstants.NewEmail)]
            public string NewEmail { get; set; }

            [Phone]
            [Display(Name = GlobalConstants.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.FirstName)]
            public string FirstName { get; set; }

            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.MiddleName)]
            public string MiddleName { get; set; }

            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.LastName)]
            public string LastName { get; set; }

            [Display(Name = GlobalConstants.BirthDate)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
            public string BirthDate { get; set; }

            [Display(Name = GlobalConstants.Gender)]
            public Gender Gender { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.CurrentPassword)]
            public string OldPassword { get; set; }

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.NewPassword)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = GlobalConstants.ConfirmNewPassword)]
            [Compare("NewPassword", ErrorMessage = ErrorMessages.PasswordsDoNotMatch)]
            public string ConfirmPassword { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            var email = await this.userManager.GetEmailAsync(user);

            this.Username = userName;
            this.Email = email;

            this.Input = new InputModel
            {
                NewEmail = email,
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                BirthDate = user.BirthDate.ToShortDateString(),
                Gender = user.Gender,
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
            await this.ValidateUser(user);

            if (this.Input.OldPassword != null && this.Input.NewPassword != null)
            {
                var changePasswordResult = await this.userManager.ChangePasswordAsync(user, this.Input.OldPassword, this.Input.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return this.Page();
                }
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set phone number.";
                    return this.RedirectToPage();
                }
            }

            this.SetUserCredentials(user);

            await this.userManager.UpdateAsync(user);
            await this.signInManager.RefreshSignInAsync(user);

            this.StatusMessage = GlobalConstants.ProfileDataUpdatedMsg;

            return this.RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var email = await this.userManager.GetEmailAsync(user);

            await this.ValidateUser(user);

            if (this.Input.NewEmail != email)
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

                this.StatusMessage = GlobalConstants.ResendEmailConfirmationInstructions;
                return this.RedirectToPage();
            }

            this.StatusMessage = GlobalConstants.EmailResetFailed;
            return this.RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.ValidateUser(user);

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

        public void SetUserCredentials(ApplicationUser user)
        {
            if (user.FirstName != this.Input.FirstName)
            {
                user.FirstName = this.Input.FirstName;
            }

            if (user.MiddleName != this.Input.MiddleName)
            {
                user.MiddleName = this.Input.MiddleName;
            }

            if (user.LastName != this.Input.LastName)
            {
                user.LastName = this.Input.LastName;
            }

            if (user.BirthDate.ToShortDateString() != this.Input.BirthDate)
            {
                user.BirthDate = DateTime.Parse(this.Input.BirthDate);
            }

            if (user.Gender != this.Input.Gender)
            {
                user.Gender = this.Input.Gender;
            }
        }

        public async Task<IActionResult> ValidateUser(ApplicationUser user)
        {
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            return null;
        }
    }
}

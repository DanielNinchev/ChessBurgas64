namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Members;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMembersService membersService;
        private readonly IMapper mapper;

        public PersonalDataModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMembersService membersService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.membersService = membersService;
            this.mapper = mapper;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.FirstName)]
            public string FirstName { get; set; }

            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.MiddleName)]
            public string MiddleName { get; set; }

            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            [Display(Name = GlobalConstants.LastName)]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = GlobalConstants.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Display(Name = GlobalConstants.BirthDate)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
            public string BirthDate { get; set; }

            [Display(Name = GlobalConstants.Gender)]
            public Gender Gender { get; set; }

            public MemberProfileModel Member { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            this.Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                BirthDate = user.BirthDate.ToShortDateString(),
                Gender = user.Gender,
            };

            this.Input.Member = this.membersService.GetByUserId<MemberProfileModel>(user.Id);
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

            if (user.Member != null)
            {
                if (user.Member.Address != this.Input.Member.Address)
                {
                    user.Member.Address = this.Input.Member.Address;
                }

                if (user.Member.School != this.Input.Member.School)
                {
                    user.Member.School = this.Input.Member.School;
                }

                if (user.Member.DateOfJoiningTheClub.ToString() != this.Input.Member.DateOfJoiningTheClub)
                {
                    user.Member.DateOfJoiningTheClub = DateTime.Parse(this.Input.Member.DateOfJoiningTheClub);
                }
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

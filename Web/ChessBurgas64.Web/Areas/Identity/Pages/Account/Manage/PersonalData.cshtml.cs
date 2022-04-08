namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Members;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class PersonalData : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMembersService membersService;

        public PersonalData(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMembersService membersService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.membersService = membersService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = GlobalConstants.FirstName)]
            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            public string FirstName { get; set; }

            [Display(Name = GlobalConstants.MiddleName)]
            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            public string MiddleName { get; set; }

            [Display(Name = GlobalConstants.LastName)]
            [StringLength(GlobalConstants.NameMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.NameMinLength)]
            public string LastName { get; set; }

            [Phone]
            [Display(Name = GlobalConstants.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Display(Name = GlobalConstants.BirthDate)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
            public string BirthDate { get; set; }

            [Display(Name = GlobalConstants.Gender)]
            public string Gender { get; set; }

            public MemberProfileModel Member { get; set; }

            [Display(Name = GlobalConstants.Address)]
            [StringLength(GlobalConstants.AddressMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.AddressMinLength)]
            public string Address { get; set; }

            [Display(Name = GlobalConstants.School)]
            [StringLength(GlobalConstants.SchoolMaxLength, ErrorMessage = "Моля, въвеждайте истинските си имена! Имената не могат да съдържат по-малко от {2} или повече от {1} символа.", MinimumLength = GlobalConstants.SchoolMinLength)]
            public string School { get; set; }

            [Display(Name = GlobalConstants.DateOfJoiningTheClub)]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
            public string DateOfJoiningTheClub { get; set; }
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

            if (this.Input.Member != null)
            {
                this.Input.Address = this.Input.Member.Address;
                this.Input.School = this.Input.Member.School;
                this.Input.DateOfJoiningTheClub = this.Input.Member.DateOfJoiningTheClub;
            }
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

            await this.SetUserCredentials(user);

            await this.userManager.UpdateAsync(user);
            await this.signInManager.RefreshSignInAsync(user);

            this.StatusMessage = GlobalConstants.ProfileDataUpdatedMsg;

            return this.RedirectToPage();
        }

        public async Task SetUserCredentials(ApplicationUser user)
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

            if (user.MemberId != null)
            {
                var member = this.membersService.GetMemberById(user.MemberId);

                if (member.Address != this.Input.Address)
                {
                    member.Address = this.Input.Address;
                }

                if (member.School != this.Input.School)
                {
                    member.School = this.Input.School;
                }

                if (member.DateOfJoiningTheClub.ToString() != this.Input.DateOfJoiningTheClub)
                {
                    member.DateOfJoiningTheClub = DateTime.Parse(this.Input.DateOfJoiningTheClub);
                }

                await this.membersService.SaveMemberChangesAsync(member);
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

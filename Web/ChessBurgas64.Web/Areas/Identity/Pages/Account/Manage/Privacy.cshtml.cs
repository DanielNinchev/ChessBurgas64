namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class Privacy : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public Privacy(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            return this.Page();
        }
    }
}
namespace ChessBurgas64.Web.Areas.Administration.Controllers
{
    using System.Text;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Messaging;
    using ChessBurgas64.Web.Areas.Identity.Pages.Account;
    using ChessBurgas64.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        //[HttpPost]
        //public async Task<IActionResult> SendPasswordChangeValidationLinkToEmail(ForgotPasswordModel input)
        //{
        //    var html = new StringBuilder();
        //    html.AppendLine($"<a href=\"/Identity/Account/Manage/ChangePassword\">{GlobalConstants.Password}</a>");
        //}
    }
}

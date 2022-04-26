namespace ChessBurgas64.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Web.ViewModels;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IEmailSender emailSender;

        public HomeController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Contacts(string statusMessage, SendEmailInputModel input)
        {
            input.StatusMessage = statusMessage;

            return this.View(input);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendEmailInputModel input)
        {
            await this.emailSender.SendEmailAsync(
                        GlobalConstants.AdminEmail,
                        input.Topic,
                        $"{input.Name}, {input.Email}, {input.Phone} {GlobalConstants.SendsTheFollowingMessage} {input.Message}");

            string statusMessage = GlobalConstants.ThankYouForYourMessage;

            return this.RedirectToAction(nameof(this.Contacts), new { statusMessage });
        }
    }
}

namespace ChessBurgas64.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using AspNetCore.ReCaptcha;
    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels;
    using ChessBurgas64.Web.ViewModels.Announcements;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IEmailSender emailSender;
        private readonly IAnnouncementsService announcementsService;

        public HomeController(IEmailSender emailSender, IAnnouncementsService announcementsService)
        {
            this.emailSender = emailSender;
            this.announcementsService = announcementsService;
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

        public async Task<IActionResult> History()
        {
            var viewModel = await this.announcementsService.GetClubHistoryAsync<SingleAnnouncementViewModel>();

            return this.View(viewModel);
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
        [ValidateReCaptcha(ErrorMessage=ErrorMessages.InvalidCaptcha)]
        public async Task<IActionResult> SendEmail(SendEmailInputModel input)
        {
            string statusMessage = GlobalConstants.ThankYouForYourMessage;

            if (!this.ModelState.IsValid)
            {
                statusMessage = ErrorMessages.InvalidCaptcha;
                return this.RedirectToAction(nameof(this.Contacts), new { statusMessage, input });
            }

            await this.emailSender.SendEmailAsync(
                        GlobalConstants.AdminEmail,
                        input.Topic,
                        $"{input.Name}, {input.Email}, {input.Phone} {GlobalConstants.SendsTheFollowingMessage} {input.Message}");

            return this.RedirectToAction(nameof(this.Contacts), new { statusMessage });
        }
    }
}

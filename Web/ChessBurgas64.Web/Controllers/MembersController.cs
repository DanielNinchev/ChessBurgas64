namespace ChessBurgas64.Web.Controllers
{
    using ChessBurgas64.Services.Data.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class MembersController : Controller
    {
        private readonly IUsersService memberService;

        public MembersController(IUsersService memberService)
        {
            this.memberService = memberService;
        }

        public IActionResult ValidateStatus()
        {
            return this.View();
        }
    }
}

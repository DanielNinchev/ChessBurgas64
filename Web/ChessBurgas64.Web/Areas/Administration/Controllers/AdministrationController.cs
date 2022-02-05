namespace ChessBurgas64.Web.Areas.Administration.Controllers
{
    using ChessBurgas64.Common;
    using ChessBurgas64.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {

    }
}

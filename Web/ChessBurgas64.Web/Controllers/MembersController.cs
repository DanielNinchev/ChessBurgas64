namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
    public class MembersController : Controller
    {
        private readonly IGroupsService groupsService;
        private readonly IMembersService membersService;

        public MembersController(
            IGroupsService groupsService,
            IMembersService membersService)
        {
            this.groupsService = groupsService;
            this.membersService = membersService;
        }

        public IActionResult AddToGroup()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToGroup(string id, GroupMemberInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.membersService.AddMemberToGroupAsync(id, input);
                await this.groupsService.InitializeGroupProperties(id);
                string controllerName = nameof(GroupsController)[..^nameof(Controller).Length];
                return this.Redirect($"/{controllerName}/{nameof(GroupsController.ById)}/{id}");
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var groupId = this.HttpContext.Session.GetString("groupId");
                await this.membersService.DeleteGroupMemberAsync(groupId, id);
                await this.groupsService.InitializeGroupProperties(groupId);
                string controllerName = nameof(GroupsController)[..^nameof(Controller).Length];
                return this.Redirect($"/{controllerName}/{nameof(GroupsController.ById)}/{groupId}");
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        public IActionResult MarkMemberAttendanceToLesson()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> MarkMemberAttendanceToLesson(string id, GroupMemberInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.membersService.AddMemberToGroupAsync(id, input);
                await this.groupsService.InitializeGroupProperties(id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            string controllerName = nameof(GroupsController)[..^nameof(Controller).Length];
            return this.Redirect($"/{controllerName}/{nameof(GroupsController.ById)}/{id}");
        }
    }
}

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
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            return this.Redirect("/Groups/ById/" + id);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var groupId = this.HttpContext.Session.GetString("groupId");
            await this.membersService.DeleteGroupMemberAsync(groupId, id);
            await this.groupsService.InitializeGroupProperties(groupId);
            return this.Redirect("/Groups/ById/" + groupId);
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

            return this.Redirect("/Groups/ById/" + id);
        }
    }
}

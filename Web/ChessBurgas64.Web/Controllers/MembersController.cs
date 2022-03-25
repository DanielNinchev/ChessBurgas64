namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

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

        public IActionResult AddToGroup(string id)
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

        //public IActionResult Edit(string id)
        //{
        //    var inputModel = this.membersService.GetById<PaymentInputModel>(id);

        //    return this.View(inputModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(string id, PaymentInputModel input)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(input);
        //    }

        //    await this.membersService.UpdateAsync(id, input);

        //    id = input.UserId;

        //    return this.RedirectToAction(nameof(UsersController.ById), "Users", new { id });
        //}

        public IActionResult MarkMemberAttendanceToLesson(string id)
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

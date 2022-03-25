namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class LessonsController : Controller
    {
        private readonly ILessonsService lessonsService;

        public LessonsController(
            ILessonsService lessonsService)
        {
            this.lessonsService = lessonsService;
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.lessonsService.GetById<LessonViewModel>(id);
            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string id, LessonInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.lessonsService.CreateAsync(input, id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            return this.Redirect("/Users/ById/" + id);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.lessonsService.DeleteAsync(id);
            var groupId = this.HttpContext.Session.GetString("groupId");
            return this.RedirectToAction("/Groups/ById/" + groupId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroupLesson(int id)
        {
            await this.lessonsService.DeleteAsync(id);
            var groupId = this.HttpContext.Session.GetString("groupId");
            return this.RedirectToAction("/Groups/ById/" + groupId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserLesson(int id)
        {
            await this.lessonsService.DeleteAsync(id);
            var userId = this.HttpContext.Session.GetString("userId");
            return this.RedirectToAction("/Users/ById/" + userId);
        }

        public IActionResult Edit(int id)
        {
            var inputModel = this.lessonsService.GetById<LessonInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LessonInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.lessonsService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(LessonsController.ById), "Lessons", new { id });
        }

        public IActionResult EditAttendants(int id)
        {
            var lessonGroupMembers = this.lessonsService.GetLessonGroupMembers<GroupMemberViewModel>(id);
            var checkboxModel = new GroupMemberCheckboxModel
            {
                GroupMembers = lessonGroupMembers,
            };

            return this.View(checkboxModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAttendants(int id, GroupMemberCheckboxModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.lessonsService.MarkLessonMemberAttendance(id, model);

            return this.RedirectToAction(nameof(LessonsController.ById), "Lessons", new { id });
        }
    }
}

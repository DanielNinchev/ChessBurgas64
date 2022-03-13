namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
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

            return this.Redirect("/Groups/ById/" + id);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.lessonsService.DeleteAsync(id);

            var groupId = this.HttpContext.Session.GetString("groupId");

            return this.RedirectToAction("/Groups/ById/" + groupId);
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

            // id = input.GroupId;

            return this.RedirectToAction(nameof(GroupsController.ById), "Groups", new { id });
        }
    }
}

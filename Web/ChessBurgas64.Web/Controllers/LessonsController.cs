namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
    public class LessonsController : Controller
    {
        private readonly ILessonsService lessonsService;

        public LessonsController(
            ILessonsService lessonsService)
        {
            this.lessonsService = lessonsService;
        }

        public async Task<IActionResult> ById(int id)
        {
            var viewModel = await this.lessonsService.GetByIdAsync<LessonViewModel>(id);
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
                string controllerName = nameof(UsersController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(UsersController.ById), controllerName, new { id });
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.lessonsService.DeleteAsync(id);
                var groupId = this.HttpContext.Session.GetString("groupId");
                string controllerName = nameof(GroupsController)[..^nameof(Controller).Length];
                return this.Redirect($"/{controllerName}/{nameof(GroupsController.ById)}/{groupId}");
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroupLesson(int id)
        {
            try
            {
                await this.lessonsService.DeleteAsync(id);
                var groupId = this.HttpContext.Session.GetString("groupId");
                string controllerName = nameof(GroupsController)[..^nameof(Controller).Length];
                return this.Redirect($"/{controllerName}/{nameof(GroupsController.ById)}/{groupId}");
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserLesson(int id)
        {
            try
            {
                await this.lessonsService.DeleteAsync(id);
                var userId = this.HttpContext.Session.GetString("userId");
                string controllerName = nameof(UsersController)[..^nameof(Controller).Length];
                return this.Redirect($"/{controllerName}/{nameof(UsersController.ById)}/{userId}");
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var inputModel = await this.lessonsService.GetByIdAsync<LessonInputModel>(id);
            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LessonInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.lessonsService.UpdateAsync(id, input);
                string controllerName = nameof(LessonsController)[..^nameof(Controller).Length];
                return this.Redirect($"/{controllerName}/{nameof(LessonsController.ById)}/{id}");
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        public async Task<IActionResult> EditAttendants(int id)
        {
            var lessonGroupMembers = await this.lessonsService.GetLessonGroupMembersAsync<GroupMemberViewModel>(id);
            var checkboxModel = new GroupMemberCheckboxModel
            {
                GroupMembers = lessonGroupMembers.ToList(),
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

            try
            {
                await this.lessonsService.MarkLessonMemberAttendanceAsync(id, model);
                string controllerName = nameof(LessonsController)[..^nameof(Controller).Length];
                return this.Redirect($"/{controllerName}/{nameof(LessonsController.ById)}/{id}");
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetLessons()
        {
            try
            {
                var draw = this.Request.Form["draw"].FirstOrDefault();
                var start = this.Request.Form["start"].FirstOrDefault(); // paging first record indicator;
                var length = this.Request.Form["length"].FirstOrDefault(); // number of displayable records;
                var sortColumn = this.Request.Form["columns[" + this.Request.Form["order[0][column]"]
                    .FirstOrDefault() + "][name]"]
                    .FirstOrDefault();
                var sortColumnDirection = this.Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = this.Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var lessonData = await this.lessonsService.GetAllLessonsTableDataAsync<LessonViewModel>(sortColumn, sortColumnDirection, searchValue);

                recordsTotal = lessonData.Count();

                var data = lessonData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };

                return this.Ok(jsonData);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IActionResult ShowLessons()
        {
            return this.View();
        }
    }
}

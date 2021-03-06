namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Groups;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using ChessBurgas64.Web.ViewModels.Members;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
    public class GroupsController : Controller
    {
        private readonly IGroupsService groupsService;
        private readonly ILessonsService lessonsService;
        private readonly IMembersService membersService;

        public GroupsController(
            IGroupsService groupsService,
            ILessonsService lessonsService,
            IMembersService membersService)
        {
            this.groupsService = groupsService;
            this.lessonsService = lessonsService;
            this.membersService = membersService;
        }

        public async Task<IActionResult> ById(string id)
        {
            var viewModel = await this.groupsService.GetByIdAsync<GroupViewModel>(id);

            this.HttpContext.Session.SetString("groupId", id);
            this.HttpContext.Session.SetString("trainerId", viewModel.Trainer.Id);

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.groupsService.CreateAsync(input);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.ShowGroups));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await this.groupsService.DeleteAsync(id);
                string controllerName = nameof(GroupsController)[..^nameof(Controller).Length];
                return this.Redirect(controllerName);
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.groupsService.GetByIdAsync<GroupInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, GroupInputModel input)
        {
            try
            {
                await this.groupsService.UpdateAsync(id, input);
                return this.RedirectToAction(nameof(this.ById), new { id });
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                var draw = this.Request.Form["draw"].FirstOrDefault();
                var start = this.Request.Form["start"].FirstOrDefault();
                var length = this.Request.Form["length"].FirstOrDefault();
                var sortColumn = this.Request.Form["columns[" + this.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = this.Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = this.Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var groupData = await this.groupsService.GetTableDataAsync<GroupTableViewModel>(sortColumn, sortColumnDirection, searchValue);

                recordsTotal = groupData.Count();

                var data = groupData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };

                return this.Ok(jsonData);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetGroupLessons()
        {
            try
            {
                var groupId = this.HttpContext.Session.GetString("groupId");
                var draw = this.Request.Form["draw"].FirstOrDefault();
                var start = this.Request.Form["start"].FirstOrDefault();
                var length = this.Request.Form["length"].FirstOrDefault();
                var sortColumn = this.Request.Form["columns[" + this.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = this.Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = this.Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var lessonData = await this.lessonsService.GetGroupLessonsTableDataAsync<LessonViewModel>(groupId, sortColumn, sortColumnDirection, searchValue);

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

        [HttpPost]
        public async Task<IActionResult> GetGroupMembers()
        {
            try
            {
                var groupId = this.HttpContext.Session.GetString("groupId");
                var draw = this.Request.Form["draw"].FirstOrDefault();
                var start = this.Request.Form["start"].FirstOrDefault();
                var length = this.Request.Form["length"].FirstOrDefault();
                var sortColumn = this.Request.Form["columns[" + this.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = this.Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = this.Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var membersData = await this.membersService.GetTableData<MemberViewModel>(groupId, sortColumn, sortColumnDirection, searchValue);

                recordsTotal = membersData.Count();

                var data = membersData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };

                return this.Ok(jsonData);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IActionResult ShowGroups()
        {
            return this.View();
        }
    }
}

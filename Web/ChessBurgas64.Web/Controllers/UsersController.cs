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
    using ChessBurgas64.Web.ViewModels.Payments;
    using ChessBurgas64.Web.ViewModels.Trainers;
    using ChessBurgas64.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.TrainerRoleName}")]
    public class UsersController : Controller
    {
        private readonly IImagesService imagesService;
        private readonly IGroupsService groupsService;
        private readonly ILessonsService lessonsService;
        private readonly IMembersService membersService;
        private readonly IPaymentsService paymentsService;
        private readonly ITrainersService trainersService;
        private readonly IUsersService usersService;
        private readonly IWebHostEnvironment environment;

        public UsersController(
            IImagesService imagesService,
            IGroupsService groupsService,
            ILessonsService lessonsService,
            IMembersService membersService,
            IPaymentsService paymentsService,
            ITrainersService trainersService,
            IUsersService usersService,
            IWebHostEnvironment environment)
        {
            this.imagesService = imagesService;
            this.groupsService = groupsService;
            this.lessonsService = lessonsService;
            this.membersService = membersService;
            this.paymentsService = paymentsService;
            this.trainersService = trainersService;
            this.usersService = usersService;
            this.environment = environment;
        }

        public async Task<IActionResult> ById(string id)
        {
            this.HttpContext.Session.SetString("userId", id);
            var viewModel = await this.usersService.GetByIdAsync<UserProfileViewModel>(id);
            return this.View(viewModel);
        }

        public async Task<IActionResult> ByMemberId(string id)
        {
            var member = await this.membersService.GetMemberByIdAsync(id);
            id = member.UserId;
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditUserInfo(string id)
        {
            var viewModel = await this.usersService.GetByIdAsync<UserInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditUserInfo(string id, UserInputModel input)
        {
            await this.usersService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        public async Task<IActionResult> EditMemberInfo(string id)
        {
            var viewModel = await this.membersService.GetByUserIdAsync<MemberInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditMemberInfo(string id, MemberInputModel input)
        {
            await this.membersService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> GetUsers()
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

                var userData = await this.usersService.GetTableDataAsync<UserTableViewModel>(sortColumn, sortColumnDirection, searchValue);

                recordsTotal = userData.Count();

                var data = userData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };

                return this.Ok(jsonData);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetUserGroups()
        {
            try
            {
                var userId = this.HttpContext.Session.GetString("userId");
                var draw = this.Request.Form["draw"].FirstOrDefault();
                var start = this.Request.Form["start"].FirstOrDefault();
                var length = this.Request.Form["length"].FirstOrDefault();
                var sortColumn = this.Request.Form["columns[" + this.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = this.Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = this.Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var groupData = await this.groupsService.GetUserGroupsTableData<GroupTableViewModel>(userId, sortColumn, sortColumnDirection, searchValue);

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
        public async Task<IActionResult> GetUserLessons()
        {
            try
            {
                var userId = this.HttpContext.Session.GetString("userId");
                var draw = this.Request.Form["draw"].FirstOrDefault();
                var start = this.Request.Form["start"].FirstOrDefault();
                var length = this.Request.Form["length"].FirstOrDefault();
                var sortColumn = this.Request.Form["columns[" + this.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = this.Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = this.Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var lessonData = await this.lessonsService.GetUserLessonsTableDataAsync<LessonViewModel>(userId, sortColumn, sortColumnDirection, searchValue);

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
        public async Task<IActionResult> GetUserPayments()
        {
            try
            {
                var userId = this.HttpContext.Session.GetString("userId");
                var draw = this.Request.Form["draw"].FirstOrDefault();
                var start = this.Request.Form["start"].FirstOrDefault();
                var length = this.Request.Form["length"].FirstOrDefault();
                var sortColumn = this.Request.Form["columns[" + this.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = this.Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = this.Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var paymentData = await this.paymentsService.GetTableData<PaymentViewModel>(userId, sortColumn, sortColumnDirection, searchValue);

                recordsTotal = paymentData.Count();

                var data = paymentData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };

                return this.Ok(jsonData);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IActionResult ShowUsers()
        {
            return this.View();
        }
    }
}

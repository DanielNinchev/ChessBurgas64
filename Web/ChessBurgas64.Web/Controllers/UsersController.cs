namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using ChessBurgas64.Web.ViewModels.Members;
    using ChessBurgas64.Web.ViewModels.Payments;
    using ChessBurgas64.Web.ViewModels.Trainers;
    using ChessBurgas64.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly ILessonsService lessonsService;
        private readonly IMembersService membersService;
        private readonly IPaymentsService paymentsService;
        private readonly ITrainersService trainersService;
        private readonly IUsersService usersService;

        public UsersController(
            ILessonsService lessonsService,
            IMembersService membersService,
            IPaymentsService paymentsService,
            ITrainersService trainersService,
            IUsersService usersService)
        {
            this.lessonsService = lessonsService;
            this.membersService = membersService;
            this.paymentsService = paymentsService;
            this.trainersService = trainersService;
            this.usersService = usersService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult ById(string id)
        {
            this.HttpContext.Session.SetString("userId", id);
            var viewModel = this.usersService.GetById<UserProfileViewModel>(id);
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult ByMemberId(string id)
        {
            var member = this.membersService.GetMemberById(id);
            id = member.UserId;
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditUserInfo(string id)
        {
            var viewModel = this.usersService.GetById<UserInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditUserInfo(string id, UserInputModel input)
        {
            await this.usersService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditMemberInfo(string id)
        {
            var viewModel = this.membersService.GetByUserId<MemberInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditMemberInfo(string id, MemberInputModel input)
        {
            await this.membersService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditTrainerInfo(string id)
        {
            var viewModel = this.trainersService.GetById<TrainerInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> EditTrainerInfo(string id, TrainerInputModel input)
        {
            await this.trainersService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        public IActionResult GetUsers()
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

                var userData = this.usersService.GetTableData<UserTableViewModel>(sortColumn, sortColumnDirection, searchValue);

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
        public IActionResult GetUserGroups()
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

                var paymentData = this.paymentsService.GetTableData<PaymentViewModel>(userId, sortColumn, sortColumnDirection, searchValue);

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

        [HttpPost]
        public IActionResult GetUserLessons()
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

                var lessonData = this.lessonsService.GetTableData<LessonViewModel>(userId, sortColumn, sortColumnDirection, searchValue);

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
        public IActionResult GetUserPayments()
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

                var paymentData = this.paymentsService.GetTableData<PaymentViewModel>(userId, sortColumn, sortColumnDirection, searchValue);

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

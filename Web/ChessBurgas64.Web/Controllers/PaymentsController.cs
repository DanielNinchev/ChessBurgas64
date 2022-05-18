namespace ChessBurgas64.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Payments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class PaymentsController : Controller
    {
        private readonly IPaymentsService paymentsService;

        public PaymentsController(
            IPaymentsService paymentsService)
        {
            this.paymentsService = paymentsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string id, PaymentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.paymentsService.CreateAsync(input, id);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            string controllerName = nameof(UsersController)[..^nameof(Controller).Length];
            return this.RedirectToAction(nameof(UsersController.ById), controllerName, new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await this.paymentsService.DeleteAsync(id);
                var userId = this.HttpContext.Session.GetString("userId");
                string controllerName = nameof(UsersController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(UsersController.ById), controllerName, new { id });
            }
            catch (Exception)
            {
                string controllerName = nameof(HomeController)[..^nameof(Controller).Length];
                return this.RedirectToAction(nameof(HomeController.Error), controllerName);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            var inputModel = await this.paymentsService.GetByIdAsync<PaymentInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PaymentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.paymentsService.UpdateAsync(id, input);
                id = input.UserId;
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                return this.View(input);
            }

            string controllerName = nameof(UsersController)[..^nameof(Controller).Length];
            return this.RedirectToAction(nameof(UsersController.ById), controllerName, new { id });
        }
    }
}

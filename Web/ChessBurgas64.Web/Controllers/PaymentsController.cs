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

            return this.Redirect("/Users/ById/" + id);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.paymentsService.DeleteAsync(id);
            var userId = this.HttpContext.Session.GetString("userId");
            return this.RedirectToAction("/Users/ById/" + userId);
        }

        public IActionResult Edit(string id)
        {
            var inputModel = this.paymentsService.GetById<PaymentInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PaymentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.paymentsService.UpdateAsync(id, input);

            id = input.UserId;

            return this.RedirectToAction(nameof(UsersController.ById), "Users", new { id });
        }
    }
}

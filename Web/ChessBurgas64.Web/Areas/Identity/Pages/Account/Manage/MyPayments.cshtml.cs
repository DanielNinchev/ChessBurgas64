namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Payments;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class MyPayments : PageModel
    {
        private readonly IPaymentsService paymentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public MyPayments(IPaymentsService paymentsService, UserManager<ApplicationUser> userManager)
        {
            this.paymentsService = paymentsService;
            this.userManager = userManager;
        }

        [BindProperty]
        public DataTables.DataTablesRequest DataTablesRequest { get; set; }

        public async Task OnGetAsync()
        {

        }

        public async Task<JsonResult> OnPostAsync()
        {
            try
            {
                var user = this.userManager.GetUserAsync(this.User).Result;

                var paymentsQuery = this.paymentsService.GetUserPaymentsTableData(user.Id);
                var recordsTotal = paymentsQuery.Count();

                var searchText = this.DataTablesRequest.Search.Value?.ToUpper();

                if (!string.IsNullOrEmpty(searchText))
                {
                    paymentsQuery = paymentsQuery.Where(g => g.Description.Contains(searchText)
                                        || g.DateOfPayment.Equals(searchText)
                                        || g.Amount.Equals(searchText));
                }

                var recordsFiltered = paymentsQuery.Count();
                var sortColumnName = this.DataTablesRequest.Columns.ElementAt(this.DataTablesRequest.Order.ElementAt(0).Column).Name;
                var sortDirection = this.DataTablesRequest.Order.ElementAt(0).Dir.ToLower();

                paymentsQuery = paymentsQuery.OrderBy($"{sortColumnName} {sortDirection}");

                var skip = this.DataTablesRequest.Start;
                var take = this.DataTablesRequest.Length;
                var data = await paymentsQuery
                    .Skip(skip)
                    .Take(take)
                    .To<PaymentViewModel>()
                    .ToListAsync();

                return new JsonResult(new
                {
                    Draw = this.DataTablesRequest.Draw,
                    RecordsTotal = recordsTotal,
                    RecordsFiltered = recordsFiltered,
                    Data = data,
                });
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

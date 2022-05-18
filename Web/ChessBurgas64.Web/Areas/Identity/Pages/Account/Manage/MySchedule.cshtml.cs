namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class MySchedule : PageModel
    {
        private readonly IGroupsService groupsService;
        private readonly UserManager<ApplicationUser> userManager;

        public MySchedule(IGroupsService groupsService, UserManager<ApplicationUser> userManager)
        {
            this.groupsService = groupsService;
            this.userManager = userManager;
        }

        public IList<Group> Groups { get; set; }

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

                var groupsQuery = this.groupsService.GetUserGroups(user.Id);
                var recordsTotal = await groupsQuery.CountAsync();

                var searchText = this.DataTablesRequest.Search.Value?.ToUpper();

                if (!string.IsNullOrEmpty(searchText))
                {
                    groupsQuery = groupsQuery.Where(g => g.Name.Contains(searchText)
                                        || g.LowestRating.Equals(searchText)
                                        || g.HighestRating.Equals(searchText)
                                        || g.Members.Count.Equals(searchText));
                }

                var recordsFiltered = groupsQuery.CountAsync();
                var sortColumnName = this.DataTablesRequest.Columns.ElementAt(this.DataTablesRequest.Order.ElementAt(0).Column).Name;
                var sortDirection = this.DataTablesRequest.Order.ElementAt(0).Dir.ToLower();

                groupsQuery = groupsQuery.OrderBy($"{sortColumnName} {sortDirection}");

                var skip = this.DataTablesRequest.Start;
                var take = this.DataTablesRequest.Length;
                var data = await groupsQuery
                    .Skip(skip)
                    .Take(take)
                    .To<GroupViewModel>()
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

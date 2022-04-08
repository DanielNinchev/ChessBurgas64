namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class MyLessons : PageModel
    {
        private readonly ILessonsService lessonsService;
        private readonly UserManager<ApplicationUser> userManager;

        public MyLessons(ILessonsService lessonsService, UserManager<ApplicationUser> userManager)
        {
            this.lessonsService = lessonsService;
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

                var lessonsQuery = this.lessonsService.GetUserLessonsTableData(user.Id);
                var recordsTotal = lessonsQuery.Count();

                var searchText = this.DataTablesRequest.Search.Value?.ToUpper();

                if (!string.IsNullOrEmpty(searchText))
                {
                    lessonsQuery = lessonsQuery.Where(g => g.Topic.Contains(searchText)
                                        || g.StartingTime.Equals(searchText)
                                        || g.Group.Name.Contains(searchText));
                }

                var recordsFiltered = lessonsQuery.Count();
                var sortColumnName = this.DataTablesRequest.Columns.ElementAt(this.DataTablesRequest.Order.ElementAt(0).Column).Name;
                var sortDirection = this.DataTablesRequest.Order.ElementAt(0).Dir.ToLower();

                lessonsQuery = lessonsQuery.OrderBy($"{sortColumnName} {sortDirection}");

                var skip = this.DataTablesRequest.Start;
                var take = this.DataTablesRequest.Length;
                var data = await lessonsQuery
                    .Skip(skip)
                    .Take(take)
                    .To<LessonViewModel>()
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

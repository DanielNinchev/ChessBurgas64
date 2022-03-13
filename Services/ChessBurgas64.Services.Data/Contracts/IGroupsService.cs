namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Web.ViewModels.Groups;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IGroupsService
    {
        Task CreateAsync(GroupInputModel input);

        IEnumerable<SelectListItem> GetAllGroups();

        T GetById<T>(string id);

        IEnumerable<T> GetTableData<T>(string sortColumn, string sortColumnDirection, string searchValue);

        Task UpdateAsync(string id, GroupInputModel input);
    }
}

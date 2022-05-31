namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IUsersService
    {
        Task DeleteAsync(ApplicationUser user);

        Task DeleteUserMemberAsync(ApplicationUser user);

        Task DeleteUserTrainerAsync(ApplicationUser user);

        IEnumerable<SelectListItem> GetAllAdminsInSelectList();

        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetTableDataAsync<T>(string sortColumn, string sortColumnDirection, string searchValue);

        Task UpdateAsync(string id, UserInputModel input);
    }
}

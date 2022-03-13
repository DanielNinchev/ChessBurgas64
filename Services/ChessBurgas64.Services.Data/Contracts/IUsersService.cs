namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task ChangeClubStatusAsync(string id, string clubStatus);

        T GetById<T>(string id);

        IEnumerable<T> GetTableData<T>(string sortColumn, string sortColumnDirection, string searchValue);

        Task UpdateAsync(string id, UserInputModel input);
    }
}

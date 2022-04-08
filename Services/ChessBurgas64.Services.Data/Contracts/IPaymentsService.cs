namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.Payments;

    public interface IPaymentsService
    {
        Task CreateAsync(PaymentInputModel input, string userId);

        Task DeleteAsync(string id);

        T GetById<T>(string id);

        IEnumerable<T> GetTableData<T>(string userId, string sortColumn, string sortColumnDirection, string searchValue);

        IQueryable<Payment> GetUserPaymentsTableData(string userId);

        Task UpdateAsync(string id, PaymentInputModel input);
    }
}

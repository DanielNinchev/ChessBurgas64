namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.NotableMembers;

    public interface INotableMembersService
    {
        Task<NotableMember> CreateAsync(NotableMemberInputModel input, string imagePath);

        Task DeleteAsync(int id);

        Task<ICollection<T>> GetAllInGovernanceAsync<T>();

        Task<ICollection<T>> GetAllPlayersAsync<T>();

        Task<T> GetByIdAsync<T>(int id);

        Task<NotableMember> GetByIdAsync(int id);

        Task<int> GetCountAsync();

        Task<NotableMember> UpdateAsync(int id, NotableMemberInputModel input);
    }
}

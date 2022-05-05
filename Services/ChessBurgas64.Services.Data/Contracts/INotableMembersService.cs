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

        ICollection<T> GetAllInGovernance<T>();

        ICollection<T> GetAllPlayers<T>();

        T GetById<T>(int id);

        NotableMember GetById(int id);

        int GetCount();

        Task<NotableMember> UpdateAsync(int id, NotableMemberInputModel input);
    }
}

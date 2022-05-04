namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.ClubPlayers;

    public interface IClubPlayersService
    {
        Task<ClubPlayer> CreateAsync(ClubPlayerInputModel input, string imagePath);

        Task DeleteAsync(int id);

        ICollection<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(int id);

        ClubPlayer GetById(int id);

        int GetCount();

        Task<ClubPlayer> UpdateAsync(int id, ClubPlayerInputModel input);
    }
}

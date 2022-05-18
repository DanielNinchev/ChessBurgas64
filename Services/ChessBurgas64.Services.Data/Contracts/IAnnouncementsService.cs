namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.Announcements;

    public interface IAnnouncementsService
    {
        Task CreateAsync(AnnouncementInputModel input, string userId, string imagePath);

        Task DeleteAsync(int id);

        Task<ICollection<T>> GetAllAsync<T>(int page, int itemsPerPage);

        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetClubHistoryAsync<T>();

        Task<int> GetCountAsync();

        Task<ICollection<T>> GetSearchedAsync<T>(IEnumerable<int> categoryIds, string searchText);

        Task InitializeAnnouncementImagesAsync(AnnouncementInputModel input, Announcement announcement, string imagePath);

        Task UpdateAsync(int id, AnnouncementInputModel input, string imagePath);
    }
}

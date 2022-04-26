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

        ICollection<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(int id);

        int GetCount();

        ICollection<T> GetSearched<T>(int page, int itemsPerPage, IEnumerable<int> categoryIds, string searchText);

        Task InitializeAnnouncementImages(AnnouncementInputModel input, Announcement announcement, string imagePath);

        Task UpdateAsync(int id, AnnouncementInputModel input, string imagePath);
    }
}

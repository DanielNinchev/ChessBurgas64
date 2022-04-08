namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.Announcements;
    using Microsoft.AspNetCore.Http;

    public interface IAnnouncementsService
    {
        Task CreateAsync(AnnouncementInputModel input, string userId, string imagePath);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(int id);

        int GetCount();

        Task<Image> InitializeAnnouncementImage(IFormFile image, Announcement announcement, string imagePath);

        Task UpdateAsync(int id, AnnouncementInputModel input);
    }
}

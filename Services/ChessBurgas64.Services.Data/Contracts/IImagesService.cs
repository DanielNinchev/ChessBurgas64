namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        Task<Image> CreateImageAsync(IFormFile image, string webRootImagePath, Image dbImage, string extension, string imagePath);

        Task DeleteAsync(string id);

        Task<Image> InitializeAnnouncementImageAsync(IFormFile image, Announcement announcement, string webRootImagePath);

        Task<Image> InitializeNotableMemberImageAsync(IFormFile image, NotableMember notableMember, string webRootImagePath);

        Task<Image> InitializePuzzleImageAsync(IFormFile image, Puzzle puzzle, string webRootImagePath);
    }
}

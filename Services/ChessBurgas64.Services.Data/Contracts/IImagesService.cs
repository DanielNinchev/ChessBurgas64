﻿namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IImagesService
    {
        Task<Image> CreateImage(IFormFile image, string webRootImagePath, Image dbImage, string extension, string imagePath);

        string GetImageExtension(IFormFile image);

        Task DeleteAsync(string id);

        Task<Image> InitializeAnnouncementImage(IFormFile image, Announcement announcement, string webRootImagePath);

        Task<Image> InitializeNotableMemberImage(IFormFile image, NotableMember notableMember, string webRootImagePath);

        Task<Image> InitializePuzzleImage(IFormFile image, Puzzle puzzle, string webRootImagePath);
    }
}

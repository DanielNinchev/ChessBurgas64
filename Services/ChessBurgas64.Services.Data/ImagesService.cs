namespace ChessBurgas64.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class ImagesService : IImagesService
    {
        private readonly IDeletableEntityRepository<NotableMember> notableMembersRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Puzzle> puzzlesRepository;

        public ImagesService(
            IDeletableEntityRepository<NotableMember> notableMembersRepository,
            IDeletableEntityRepository<Image> imagesRepository,
            IDeletableEntityRepository<Puzzle> puzzlesRepository)
        {
            this.notableMembersRepository = notableMembersRepository;
            this.imagesRepository = imagesRepository;
            this.puzzlesRepository = puzzlesRepository;
        }

        public async Task<Image> CreateImageAsync(IFormFile image, string webRootImagePath, Image dbImage, string extension, string imagePath)
        {
            var physicalPath = $"{webRootImagePath}{dbImage.Id}{extension}";

            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);

            await image.CopyToAsync(fileStream);

            dbImage.ImageUrl = $"{imagePath}{dbImage.Id}{extension}";

            await this.imagesRepository.AddAsync(dbImage);
            await this.imagesRepository.SaveChangesAsync();

            return dbImage;
        }

        public async Task DeleteAsync(string id)
        {
            var image = this.imagesRepository.All().FirstOrDefault(x => x.Id == id);
            this.imagesRepository.Delete(image);
            await this.puzzlesRepository.SaveChangesAsync();
        }

        public async Task<Image> InitializeAnnouncementImageAsync(IFormFile image, Announcement announcement, string webRootImagePath)
        {
            var extension = this.GetImageExtension(image);
            var dbImage = new Image
            {
                AnnouncementId = announcement.Id,
                Announcement = announcement,
                Extension = extension,
            };

            var newImage = await this.CreateImageAsync(image, webRootImagePath, dbImage, extension, GlobalConstants.AnnouncementImagesPath);

            return newImage;
        }

        public async Task<Image> InitializeNotableMemberImageAsync(IFormFile image, NotableMember notableMember, string webRootImagePath)
        {
            var extension = this.GetImageExtension(image);
            var dbImage = new Image
            {
                NotableMemberId = notableMember.Id,
                NotableMember = notableMember,
                Extension = extension,
            };

            var oldImage = await this.imagesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.NotableMemberId == notableMember.Id);

            if (oldImage != null)
            {
                this.imagesRepository.HardDelete(oldImage);
            }

            notableMember.Image = await this.CreateImageAsync(image, webRootImagePath, dbImage, extension, GlobalConstants.NotableMembersImagesPath);
            notableMember.ImageId = dbImage.Id;

            await this.notableMembersRepository.SaveChangesAsync();

            return notableMember.Image;
        }

        public async Task<Image> InitializePuzzleImageAsync(IFormFile image, Puzzle puzzle, string webRootImagePath)
        {
            var extension = this.GetImageExtension(image);
            var dbImage = new Image
            {
                PuzzleId = puzzle.Id,
                Puzzle = puzzle,
                Extension = extension,
            };

            var oldImage = await this.imagesRepository
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.PuzzleId == puzzle.Id);

            if (oldImage != null)
            {
                this.imagesRepository.HardDelete(oldImage);
            }

            puzzle.Image = await this.CreateImageAsync(image, webRootImagePath, dbImage, extension, GlobalConstants.PuzzleImagesPath);
            puzzle.ImageId = dbImage.Id;

            await this.puzzlesRepository.SaveChangesAsync();

            return puzzle.Image;
        }

        private string GetImageExtension(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName);

            if (!GlobalConstants.AllowedImageExtensions.Any(x => extension.ToLower().EndsWith(x)))
            {
                throw new InvalidDataException($"{ErrorMessages.InvalidImageExtension}{extension}");
            }

            return extension;
        }
    }
}

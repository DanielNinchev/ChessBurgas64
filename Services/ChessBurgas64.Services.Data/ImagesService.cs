﻿namespace ChessBurgas64.Services.Data
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

    public class ImagesService : IImagesService
    {
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Puzzle> puzzlesRepository;

        public ImagesService(
            IDeletableEntityRepository<Image> imagesRepository,
            IDeletableEntityRepository<Puzzle> puzzlesRepository)
        {
            this.imagesRepository = imagesRepository;
            this.puzzlesRepository = puzzlesRepository;
        }

        public async Task<Image> CreateImage(IFormFile image, string webRootImagePath, Image dbImage, string extension, string imagePath)
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

        public string GetImageExtension(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName);

            if (!GlobalConstants.AllowedImageExtensions.Any(x => extension.ToLower().EndsWith(x)))
            {
                throw new Exception($"{ErrorMessages.InvalidImageExtension}{extension}");
            }

            return extension;
        }

        public async Task<Image> InitializePuzzleImage(IFormFile image, Puzzle puzzle, string webRootImagePath)
        {
            var extension = this.GetImageExtension(image);

            var dbImage = new Image
            {
                PuzzleId = puzzle.Id,
                Puzzle = puzzle,
                Extension = extension,
            };

            if (puzzle.ImageId != null)
            {
                var oldImage = this.imagesRepository.All().FirstOrDefault(x => x.Id == puzzle.ImageId);
                this.imagesRepository.HardDelete(oldImage);
            }

            puzzle.Image = await this.CreateImage(image, webRootImagePath, dbImage, extension, GlobalConstants.PuzzleImagesPath);
            puzzle.ImageId = dbImage.Id;

            await this.puzzlesRepository.SaveChangesAsync();

            return puzzle.Image;
        }

        public async Task<Image> InitializeTrainerImage(IFormFile image, Trainer trainer, string webRootImagePath)
        {
            var extension = this.GetImageExtension(image);

            var dbImage = new Image
            {
                TrainerId = trainer.Id,
                Trainer = trainer,
                Extension = extension,
            };

            if (trainer.ImageId != null)
            {
                var oldImage = this.imagesRepository.All().FirstOrDefault(x => x.Id == trainer.ImageId);
                this.imagesRepository.HardDelete(oldImage);
            }

            return await this.CreateImage(image, webRootImagePath, dbImage, extension, GlobalConstants.TrainerImagesPath);
        }
    }
}
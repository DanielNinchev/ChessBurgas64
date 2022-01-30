namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Http;

    public class AnnouncementsService : IAnnouncementsService
    {
        private readonly IDeletableEntityRepository<Announcement> announcementsRepository;

        public AnnouncementsService(IDeletableEntityRepository<Announcement> announcementsRepository)
        {
            this.announcementsRepository = announcementsRepository;
        }

        public async Task CreateAsync(CreateAnnouncementInputModel input, string userId, string imagePath)
        {
            // TODO: use mapping
            var announcement = new Announcement
            {
                Title = input.Title,
                Date = DateTime.Now,
                Text = input.Text,
                AuthorId = userId,
                CategoryId = input.CategoryId,
            };

            Directory.CreateDirectory(imagePath);

            var mainImage = await this.InitializeAnnouncementImage(input.MainImage, announcement, imagePath);

            announcement.MainImageUrl = $"{GlobalConstants.AnnouncementImagesPath}{mainImage.Id}{mainImage.Extension}";

            foreach (var image in input.AdditionalImages)
            {
                var dbImage = await this.InitializeAnnouncementImage(image, announcement, imagePath);

                announcement.Images.Add(dbImage);
            }

            await this.announcementsRepository.AddAsync(announcement);
            await this.announcementsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var announcements = this.announcementsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return announcements;
        }

        public T GetById<T>(int id)
        {
            var announcement = this.announcementsRepository.AllAsNoTracking().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return announcement;
        }

        public int GetCount()
        {
            return this.announcementsRepository.All().Count();
        }

        public async Task<Image> InitializeAnnouncementImage(IFormFile image, Announcement announcement, string imagePath)
        {
            var extension = Path.GetExtension(image.FileName);

            if (!GlobalConstants.AllowedImageExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"{ErrorMessages.InvalidImageExtension}{extension}");
            }

            var dbImage = new Image
            {
                Announcement = announcement,
                Extension = extension,
            };

            var physicalPath = $"{imagePath}{dbImage.Id}{extension}";

            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);

            await image.CopyToAsync(fileStream);

            dbImage.ImageUrl = $"{GlobalConstants.AnnouncementImagesPath}{dbImage.Id}{extension}";

            return dbImage;
        }
    }
}

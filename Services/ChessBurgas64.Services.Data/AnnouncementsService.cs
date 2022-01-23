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

            foreach (var image in input.Images)
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

                announcement.Images.Add(dbImage);

                var physicalPath = $"{imagePath}{dbImage.Id}{extension}";

                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);

                await image.CopyToAsync(fileStream);
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

        public int GetCount()
        {
            return this.announcementsRepository.All().Count();
        }
    }
}

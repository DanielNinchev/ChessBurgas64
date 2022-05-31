namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Announcements;
    using Microsoft.EntityFrameworkCore;

    public class AnnouncementsService : IAnnouncementsService
    {
        private readonly IDeletableEntityRepository<Announcement> announcementsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IImagesService imagesService;
        private readonly IMapper mapper;

        public AnnouncementsService(
            IDeletableEntityRepository<Announcement> announcementsRepository,
            IDeletableEntityRepository<Image> imagesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IImagesService imagesService,
            IMapper mapper)
        {
            this.announcementsRepository = announcementsRepository;
            this.imagesRepository = imagesRepository;
            this.usersRepository = usersRepository;
            this.imagesService = imagesService;
            this.mapper = mapper;
        }

        public async Task CreateAsync(AnnouncementInputModel input, string userId, string imagePath)
        {
            var announcement = this.mapper.Map<Announcement>(input);

            await this.announcementsRepository.AddAsync(announcement);
            await this.announcementsRepository.SaveChangesAsync();
            await this.InitializeAnnouncementImagesAsync(input, announcement, imagePath);
        }

        public async Task DeleteAsync(int id)
        {
            var announcement = this.announcementsRepository.All().FirstOrDefault(x => x.Id == id);
            this.announcementsRepository.Delete(announcement);
            await this.announcementsRepository.SaveChangesAsync();
        }

        public async Task<ICollection<T>> GetAllAsync<T>(int page, int itemsPerPage)
        {
            var announcements = await this.announcementsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Date)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();

            return announcements;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var announcement = await this.announcementsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return announcement;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.announcementsRepository.AllAsNoTracking().CountAsync();
        }

        public async Task<T> GetClubHistoryAsync<T>()
        {
            var announcement = await this.announcementsRepository
                .AllAsNoTracking()
                .Where(x => x.Title == GlobalConstants.HistoryOfChessInBurgas)
                .To<T>()
                .FirstOrDefaultAsync();

            return announcement;
        }

        public async Task<ICollection<T>> GetSearchedAsync<T>(IEnumerable<int> categoryIds, string searchText)
        {
            var announcements = this.announcementsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Date)
                .AsQueryable();

            if (categoryIds.Any() && searchText != null)
            {
                foreach (var categoryId in categoryIds)
                {
                    announcements = announcements.Where(x => categoryIds.Any(id => id == x.CategoryId)
                                                        && (x.Title.ToLower().Contains(searchText.ToLower())
                                                            || x.Text.ToLower().Contains(searchText.ToLower())
                                                            || x.Description.ToLower().Contains(searchText.ToLower())
                                                            || searchText.ToLower().Contains(x.Title.ToLower())
                                                            || searchText.ToLower().Contains(x.Description.ToLower())));
                }
            }
            else if (!categoryIds.Any() && searchText != null)
            {
                announcements = announcements.Where(x => x.Title.ToLower().Contains(searchText.ToLower())
                                                           || x.Text.ToLower().Contains(searchText.ToLower())
                                                           || x.Description.ToLower().Contains(searchText.ToLower())
                                                           || searchText.ToLower().Contains(x.Title.ToLower())
                                                           || searchText.ToLower().Contains(x.Description.ToLower()));
            }
            else if (categoryIds.Any() && searchText == null)
            {
                foreach (var categoryId in categoryIds)
                {
                    announcements = announcements.Where(x => categoryIds.Any(id => id == x.CategoryId));
                }
            }
            else
            {
                return null;
            }

            return await announcements.To<T>().ToListAsync();
        }

        public async Task InitializeAnnouncementImagesAsync(AnnouncementInputModel input, Announcement announcement, string imagePath)
        {
            Directory.CreateDirectory(imagePath);

            var dbAnnouncementImages = this.imagesRepository.All().Where(x => x.AnnouncementId == announcement.Id);
            if (dbAnnouncementImages != null)
            {
                foreach (var image in dbAnnouncementImages)
                {
                    this.imagesRepository.HardDelete(image);
                }
            }

            var mainImage = await this.imagesService.InitializeAnnouncementImageAsync(input.MainImage, announcement, imagePath);
            announcement.MainImageUrl = $"{GlobalConstants.AnnouncementImagesPath}{mainImage.Id}{mainImage.Extension}";

            if (input.AdditionalImages != null)
            {
                foreach (var image in input.AdditionalImages)
                {
                    var dbImage = await this.imagesService.InitializeAnnouncementImageAsync(image, announcement, imagePath);
                    announcement.Images.Add(dbImage);
                }
            }

            await this.announcementsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AnnouncementInputModel input, string imagePath)
        {
            var announcement = await this.announcementsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            announcement.AuthorId = input.AuthorId;
            announcement.CategoryId = input.CategoryId;
            announcement.Date = DateTime.Parse(input.Date.ToShortDateString());
            announcement.Description = input.Description;
            announcement.Text = input.Text;
            announcement.Title = input.Title;

            await this.InitializeAnnouncementImagesAsync(input, announcement, imagePath);
        }
    }
}

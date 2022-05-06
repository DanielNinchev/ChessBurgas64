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

    public class AnnouncementsService : IAnnouncementsService
    {
        private readonly IDeletableEntityRepository<Announcement> announcementsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IImagesService imagesService;
        private readonly IMapper mapper;

        public AnnouncementsService(
            IDeletableEntityRepository<Announcement> announcementsRepository,
            IDeletableEntityRepository<Image> imagesRepository,
            IImagesService imagesService,
            IMapper mapper)
        {
            this.announcementsRepository = announcementsRepository;
            this.imagesRepository = imagesRepository;
            this.imagesService = imagesService;
            this.mapper = mapper;
        }

        public async Task CreateAsync(AnnouncementInputModel input, string userId, string imagePath)
        {
            var announcement = this.mapper.Map<Announcement>(input);

            await this.announcementsRepository.AddAsync(announcement);
            await this.announcementsRepository.SaveChangesAsync();
            await this.InitializeAnnouncementImages(input, announcement, imagePath);
        }

        public async Task DeleteAsync(int id)
        {
            var announcement = this.announcementsRepository.All().FirstOrDefault(x => x.Id == id);

            this.announcementsRepository.Delete(announcement);

            await this.announcementsRepository.SaveChangesAsync();
        }

        public ICollection<T> GetAll<T>(int page, int itemsPerPage)
        {
            var announcements = this.announcementsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Date)
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
            return this.announcementsRepository.AllAsNoTracking().Count();
        }

        public T GetClubHistory<T>()
        {
            var announcement = this.announcementsRepository.AllAsNoTracking().Where(x => x.Title == GlobalConstants.HistoryOfChessInBurgas)
                .To<T>().FirstOrDefault();

            return announcement;
        }

        public ICollection<T> GetSearched<T>(int page, int itemsPerPage, IEnumerable<int> categoryIds, string searchText)
        {
            var announcements = this.announcementsRepository.All()
                .OrderByDescending(x => x.Date)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            if (categoryIds != null && searchText != null)
            {
                foreach (var categoryId in categoryIds)
                {
                    announcements = announcements.Where(x => categoryIds.Any(id => id == x.CategoryId)
                                                        && (x.Title.ToLower().Contains(searchText.ToLower())
                                                            || x.Text.ToLower().Contains(searchText.ToLower())
                                                            || x.Description.ToLower().Contains(searchText.ToLower())));
                }
            }
            else if (categoryIds == null && searchText != null)
            {
                announcements = announcements.Where(x => x.Title.ToLower().Contains(searchText.ToLower())
                                                           || x.Text.ToLower().Contains(searchText.ToLower())
                                                           || x.Description.ToLower().Contains(searchText.ToLower()));
            }
            else if (categoryIds != null && searchText == null)
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

            return announcements.To<T>().ToList();
        }

        public async Task InitializeAnnouncementImages(AnnouncementInputModel input, Announcement announcement, string imagePath)
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

            var mainImage = await this.imagesService.InitializeAnnouncementImage(input.MainImage, announcement, imagePath);
            announcement.MainImageUrl = $"{GlobalConstants.AnnouncementImagesPath}{mainImage.Id}{mainImage.Extension}";

            if (input.AdditionalImages != null)
            {
                foreach (var image in input.AdditionalImages)
                {
                    var dbImage = await this.imagesService.InitializeAnnouncementImage(image, announcement, imagePath);
                    announcement.Images.Add(dbImage);
                }
            }

            await this.announcementsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AnnouncementInputModel input, string imagePath)
        {
            var announcement = this.announcementsRepository.All().FirstOrDefault(x => x.Id == id);

            announcement.CategoryId = input.CategoryId;
            announcement.Date = DateTime.Parse(input.Date.ToShortDateString());
            announcement.Description = input.Description;
            announcement.Text = input.Text;
            announcement.Title = input.Title;

            await this.InitializeAnnouncementImages(input, announcement, imagePath);
        }
    }
}

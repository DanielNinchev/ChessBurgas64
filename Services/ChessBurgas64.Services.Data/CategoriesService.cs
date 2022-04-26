namespace ChessBurgas64.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<AnnouncementCategory> announcementCategoriesRepository;
        private readonly IDeletableEntityRepository<PuzzleCategory> puzzleCategoriesRepository;
        private readonly IDeletableEntityRepository<VideoCategory> videoCategoriesRepository;

        public CategoriesService(
            IDeletableEntityRepository<AnnouncementCategory> announcementCategoriesRepository,
            IDeletableEntityRepository<PuzzleCategory> puzzleCategoriesRepository,
            IDeletableEntityRepository<VideoCategory> videoCategoriesRepository)
        {
            this.announcementCategoriesRepository = announcementCategoriesRepository;
            this.puzzleCategoriesRepository = puzzleCategoriesRepository;
            this.videoCategoriesRepository = videoCategoriesRepository;
        }

        public IEnumerable<T> GetAllAnnouncementCategories<T>()
        {
            return this.announcementCategoriesRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllPuzzleCategories<T>()
        {
            return this.puzzleCategoriesRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllVideoCategories<T>()
        {
            return this.videoCategoriesRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();
        }

        public IEnumerable<SelectListItem> GetAnnouncementCategoriesInSelectList()
        {
            return this.announcementCategoriesRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }

        public IEnumerable<SelectListItem> GetPuzzleCategoriesInSelectList()
        {
            return this.puzzleCategoriesRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }

        public IEnumerable<SelectListItem> GetVideoCategoriesInSelectList()
        {
            return this.videoCategoriesRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()));
        }
    }
}

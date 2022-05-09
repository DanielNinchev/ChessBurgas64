namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Categories;
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

        public IEnumerable<T> GetCategoriesByIds<T>(IEnumerable<int> ids, string controllerName)
        {
            if (controllerName.StartsWith(nameof(Announcement)))
            {
                return this.announcementCategoriesRepository.AllAsNoTracking()
                    .Where(x => ids.Contains(x.Id))
                    .OrderBy(x => x.Name)
                    .To<T>()
                    .ToList();
            }
            else if (controllerName.StartsWith(nameof(Puzzle)))
            {
                return this.puzzleCategoriesRepository.AllAsNoTracking()
                    .Where(x => ids.Contains(x.Id))
                    .OrderBy(x => x.Name)
                    .To<T>()
                    .ToList();
            }
            else if (controllerName.StartsWith(nameof(Video)))
            {
                return this.videoCategoriesRepository.AllAsNoTracking()
                    .Where(x => ids.Contains(x.Id))
                    .OrderBy(x => x.Name)
                    .To<T>()
                    .ToList();
            }
            else
            {
                return null;
            }
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

        public void InitializeSearchedParameters(SearchInputModel input, IDictionary<string, string> parms)
        {
            // If the user searches by categories from the Search view pass the category ids to categoryIds
            var searchTextExistsInParms = parms.ContainsKey("SearchText");
            var categoriesCount = searchTextExistsInParms ? parms.Count - 1 : parms.Count; // making sure the last list member (the searchText) is not reached
            var parmValues = parms.Values.ToArray(); // the parameter values that come from _PagingPartial (including the searchText)
            var categoryIds = new List<int>();

            if (searchTextExistsInParms)
            {
                input.SearchText = parms["SearchText"];
            }

            for (int i = 0; i < categoriesCount; i++)
            {
                if (parms.Values.ToArray()[i] != null)
                {
                    categoryIds.Add(int.Parse(parmValues[i]));
                }
            }

            input.Categories = categoryIds.ToArray();
        }
    }
}

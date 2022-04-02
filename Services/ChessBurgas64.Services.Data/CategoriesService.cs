namespace ChessBurgas64.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Puzzle> puzzlesRepository;

        public CategoriesService(
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Puzzle> puzzlesRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.puzzlesRepository = puzzlesRepository;
        }

        public IEnumerable<SelectListItem> GetAnnouncementCategories()
        {
            return this.categoriesRepository.AllAsNoTracking()
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

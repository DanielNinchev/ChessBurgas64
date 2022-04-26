namespace ChessBurgas64.Web.ViewComponents
{
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Announcements;
    using ChessBurgas64.Web.ViewModels.Categories;
    using ChessBurgas64.Web.ViewModels.Puzzles;
    using ChessBurgas64.Web.ViewModels.Videos;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesViewComponent(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IViewComponentResult Invoke(string title)
        {
            IEnumerable<SelectListItem> categories = null;
            CategoryInputModel viewModel = null;

            if (title.StartsWith(nameof(Announcement)))
            {
                categories = this.categoriesService.GetAnnouncementCategoriesInSelectList();
                viewModel = new AnnouncementInputModel
                {
                    Categories = categories,
                };
            }
            else if (title.StartsWith(nameof(Puzzle)))
            {
                categories = this.categoriesService.GetPuzzleCategoriesInSelectList();
                viewModel = new PuzzleInputModel
                {
                    Categories = categories,
                };
            }
            else if (title.StartsWith(nameof(Video)))
            {
                categories = this.categoriesService.GetVideoCategoriesInSelectList();
                viewModel = new VideoInputModel
                {
                    Categories = categories,
                };
            }

            return this.View(viewModel);
        }
    }
}

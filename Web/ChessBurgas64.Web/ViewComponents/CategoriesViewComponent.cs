namespace ChessBurgas64.Web.ViewComponents
{
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesViewComponent(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IViewComponentResult Invoke(string title)
        {
            var categories = this.categoriesService.GetAllCategories();
            var viewModel = new CreateAnnouncementInputModel
            {
                Title = title,
                Categories = categories,
            };

            return this.View(viewModel);
        }
    }
}

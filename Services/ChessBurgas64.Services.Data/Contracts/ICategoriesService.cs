namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;

    using ChessBurgas64.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAllAnnouncementCategories<T>();

        IEnumerable<T> GetAllPuzzleCategories<T>();

        IEnumerable<T> GetAllVideoCategories<T>();

        IEnumerable<T> GetCategoriesByIds<T>(IEnumerable<int> ids, string controllerName);

        IEnumerable<SelectListItem> GetAnnouncementCategoriesInSelectList();

        IEnumerable<SelectListItem> GetPuzzleCategoriesInSelectList();

        IEnumerable<SelectListItem> GetVideoCategoriesInSelectList();

        void InitializeSearchedParameters(SearchInputModel input, IDictionary<string, string> parms);
    }
}

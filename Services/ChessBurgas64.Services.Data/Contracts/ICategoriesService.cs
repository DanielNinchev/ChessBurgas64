namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        Task<IEnumerable<T>> GetAllAnnouncementCategoriesAsync<T>();

        Task<IEnumerable<T>> GetAllPuzzleCategoriesAsync<T>();

        Task<IEnumerable<T>> GetAllVideoCategoriesAsync<T>();

        Task<IEnumerable<T>> GetCategoriesByIdsAsync<T>(IEnumerable<int> ids, string controllerName);

        IEnumerable<SelectListItem> GetAnnouncementCategoriesInSelectList();

        IEnumerable<SelectListItem> GetPuzzleCategoriesInSelectList();

        IEnumerable<SelectListItem> GetVideoCategoriesInSelectList();

        void InitializeSearchedParameters(SearchInputModel input, IDictionary<string, string> parms);
    }
}

namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAllAnnouncementCategories<T>();

        IEnumerable<T> GetAllPuzzleCategories<T>();

        IEnumerable<T> GetAllVideoCategories<T>();

        IEnumerable<SelectListItem> GetAnnouncementCategoriesInSelectList();

        IEnumerable<SelectListItem> GetPuzzleCategoriesInSelectList();

        IEnumerable<SelectListItem> GetVideoCategoriesInSelectList();
    }
}

namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        IEnumerable<SelectListItem> GetAllCategories();
    }
}

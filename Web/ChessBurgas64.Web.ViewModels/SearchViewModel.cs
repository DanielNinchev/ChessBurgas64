namespace ChessBurgas64.Web.ViewModels
{
    using System.Collections.Generic;

    using ChessBurgas64.Web.ViewModels.Categories;

    public class SearchViewModel
    {
        public string SearchText { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}

namespace ChessBurgas64.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    public class SearchInputModel
    {
        public string SearchText { get; set; }

        public IEnumerable<int> Categories { get; set; }
    }
}

namespace ChessBurgas64.Web.ViewModels
{
    using System;

    using ChessBurgas64.Web.ViewModels.Categories;

    public abstract class EntityInListViewModel
    {
        public bool IsSearched { get; set; }

        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.Count / this.ItemsPerPage);

        public int Count { get; set; }

        public SearchViewModel Search { get; set; }
    }
}

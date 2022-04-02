namespace ChessBurgas64.Web.ViewModels
{
    using System;

    public abstract class EntityInListViewModel
    {
        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.Count / this.ItemsPerPage);

        public int Count { get; set; }
    }
}

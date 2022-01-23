namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using System;
    using System.Collections.Generic;

    public class AnnouncementsListViewModel
    {
        public IEnumerable<AnnouncementInCardViewModel> Announcements { get; set; }

        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.AnnouncementsCount / this.ItemsPerPage);

        public int AnnouncementsCount { get; set; }
    }
}

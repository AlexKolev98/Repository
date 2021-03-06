namespace TheGreatGrape.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int ItemsPerPage { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.ItemsCount / this.ItemsPerPage);

        public int ItemsCount { get; set; }

        public string SearchBy { get; set; }

        public string SearchByInput { get; set; }
    }
}

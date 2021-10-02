using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheGreatGrape.Web.ViewModels.Wines;

namespace TheGreatGrape.Web.ViewModels
{

    public class ItemListViewModel : PagingViewModel
    {
        public ItemListViewModel()
        {
            this.OrderBy = new WineOrderByInputModel();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name ="Order by:")]
        public WineOrderByInputModel OrderBy { get; set; }
    }
}

// Base class for view models that use paging.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheGreatGrape.Web.ViewModels.Wines;

namespace TheGreatGrape.Web.ViewModels.Search
{
    public class SearchInputModel
    {
        public SearchInputModel()
        {
            this.SearchItems = new List<SearchByItemsViewModel>
            {
                new SearchByItemsViewModel {Text = "Name", Value = "Name" },
                new SearchByItemsViewModel {Text = "Price", Value = "Price" },
                new SearchByItemsViewModel {Text = "Volume", Value = "Volume" },
                new SearchByItemsViewModel {Text = "Alcohol %", Value = "Alcohol" },
                new SearchByItemsViewModel {Text = "Country", Value = "Country" },
                new SearchByItemsViewModel {Text = "Winery", Value = "Winery" },
                new SearchByItemsViewModel {Text = "Grape", Value = "Grape" },
                new SearchByItemsViewModel {Text = "Year", Value = "Year" },
            };
        }

        public IEnumerable<SearchByItemsViewModel> SearchItems { get; set; }

        [Display(Name = "Search by:")]
        public string SearchBy { get; set; }

        [Display(Name = "Enter text:")]
        [Required]
        public string SearchByInput { get; set; }
    }
}

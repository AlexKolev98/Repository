namespace TheGreatGrape.Web.ViewModels
{

    public class ItemListViewModel : PagingViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

// Base class for view models that use paging.

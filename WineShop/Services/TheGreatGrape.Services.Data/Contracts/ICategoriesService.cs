namespace TheGreatGrape.Services.Data
{
    using System.Collections.Generic;

    using TheGreatGrape.Web.ViewModels.Home;

    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        public GetAllCategoriesViewModel GetCategories();
    }
}

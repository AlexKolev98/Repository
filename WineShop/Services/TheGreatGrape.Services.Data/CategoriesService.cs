namespace TheGreatGrape.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using TheGreatGrape.Web.ViewModels.Home;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public GetAllCategoriesViewModel GetCategories()
        {
            var data = new GetAllCategoriesViewModel
            {
                Categories = this.categoriesRepository.All().ToList(),
            };
            return data;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.categoriesRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).OrderByDescending(x => x.Name)
                .ToList()
            .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}

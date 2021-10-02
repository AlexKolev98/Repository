using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Data.Models.TheGreatGrape.Models;
using TheGreatGrape.Web.ViewModels.Home;

namespace TheGreatGrape.Services.Data
{
    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public GetCategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IndexViewModel GetCategories()
        {
            var data = new IndexViewModel
            {
                Categories = this.categoriesRepository.All().ToList(),
            };
            return data;
        }
    }
}

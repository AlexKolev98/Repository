using System;
using System.Collections.Generic;
using System.Text;
using TheGreatGrape.Web.ViewModels.Home;

namespace TheGreatGrape.Services.Data
{
    public interface IGetCategoriesService
    {
        IndexViewModel GetCategories();
    }
}

namespace TheGreatGrape.Web.Controllers
{
    using System.Diagnostics;

    using TheGreatGrape.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TheGreatGrape.Web.ViewModels.Home;
    using TheGreatGrape.Data;
    using System.Linq;
    using TheGreatGrape.Data.Common.Models;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Services.Data;

    public class HomeController : BaseController
    {
        private readonly IGetCategoriesService categoriesService;

        public HomeController(IGetCategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var returnView = this.categoriesService.GetCategories();
            return this.View(returnView);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

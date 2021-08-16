namespace TheGreatGrape.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TheGreatGrape.Data.Models;
    using TheGreatGrape.Services.Data;
    using TheGreatGrape.Web.ViewModels.Search;

    public class SearchController : Controller
    {
        private readonly IWinesService winesService;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly int ItemsPerPage = 12;
        private readonly string isComingFrom = "Search";

        public SearchController(
            IWinesService winesService,
            UserManager<ApplicationUser> userManager)
        {
            this.winesService = winesService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Wines()
        {
            var viewModel = new SearchInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Wines(SearchInputModel input)
        {
            int page = 1;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var viewModel = this.winesService.GetAllByX(page, this.ItemsPerPage, input.SearchBy, input.SearchByInput, this.isComingFrom);

            this.TempData["viewModel"] = viewModel;

            return this.RedirectToAction("AllByX", "Wines");
        }
    }
}

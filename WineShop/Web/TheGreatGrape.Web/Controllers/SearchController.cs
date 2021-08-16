namespace TheGreatGrape.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using TheGreatGrape.Data.Models;
    using TheGreatGrape.Services.Data;
    using TheGreatGrape.Web.ViewModels.Search;
    using TheGreatGrape.Web.ViewModels.Wines;

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

            var viewModel = new WinesListViewModel
            {
                PageNumber = page,
                ItemsPerPage = this.ItemsPerPage,
                ItemsCount = this.winesService.GetCount(),
                Wines = this.winesService.GetAllByX(page, this.ItemsPerPage, input.SearchByInput, input.SearchBy, this.isComingFrom),
            };

            this.TempData["viewModel"] = JsonConvert.SerializeObject(viewModel);

            return this.RedirectToAction("AllByX", "Wines");
        }
    }
}

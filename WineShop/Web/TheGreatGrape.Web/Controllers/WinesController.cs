namespace TheGreatGrape.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TheGreatGrape.Data.Models;
    using TheGreatGrape.Services.Data;
    using TheGreatGrape.Services.Data.Create;
    using TheGreatGrape.Web.ViewModels;
    using TheGreatGrape.Web.ViewModels.Wines;
    using TheGreatGrape.Web.ViewModels.Wines.Create;

    public class WinesController : BaseController
    {
        private readonly IWinesService winesService;
        private readonly IGrapesService grapesService;
        private readonly ICategoriesService categoriesService;
        private readonly IWineriesService wineriesService;
        private readonly ICreateWineService createWineService;
        private readonly ICountriesService countriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        private readonly int itemsPerPage = 12;
        private readonly string isComingFrom = nameof(WinesController);

        public WinesController(
            IWinesService winesService,
            IGrapesService grapesService,
            ICategoriesService categoriesService,
            IWineriesService wineriesService,
            ICreateWineService createWineService,
            ICountriesService countriesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.winesService = winesService;
            this.grapesService = grapesService;
            this.categoriesService = categoriesService;
            this.wineriesService = wineriesService;
            this.createWineService = createWineService;
            this.countriesService = countriesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Index(int id = 1)
        {
            return this.Redirect("/Wines/All");
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateWineInputModel();
            viewModel.Categories = this.categoriesService.GetAllAsKeyValuePairs();
            viewModel.Wineries = this.wineriesService.GetAllAsKeyValuePairs();
            viewModel.Grapes = this.grapesService.GetAllAsKeyValuePairs();
            viewModel.Countries = this.countriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateWineInputModel input)
        {
            bool isApproved = false;
            if (this.User.IsInRole("Administrator"))
            {
                isApproved = true;
            }

            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAllAsKeyValuePairs();
                input.Wineries = this.wineriesService.GetAllAsKeyValuePairs();
                input.Grapes = this.grapesService.GetAllAsKeyValuePairs();
                input.Countries = this.countriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.createWineService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images", isApproved);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.Redirect("/");
        }

        public IActionResult AllByX(string searchByInput, string searchBy, int itemId, int pageNumberId = 1)
        {
            WinesListViewModel viewModelFromSearch = (WinesListViewModel)this.TempData["viewModel"];
            if (searchByInput != null && searchBy != null)
            {
                var viewModel = new WinesListViewModel
                {
                    PageNumber = pageNumberId,
                    ItemsCount = this.winesService.GetCount(),
                    ItemsPerPage = this.itemsPerPage,
                    Wines = this.winesService.GetAllByX(pageNumberId, this.itemsPerPage, searchByInput, searchBy, this.isComingFrom),
                };
                if (viewModel.Wines == null)
                {
                    this.Redirect("/Wines/" + nameof(this.NothingFound));
                }

                return this.View(viewModel);
            }

            if (viewModelFromSearch == null)
            {
                return this.Redirect("/Wines/" + nameof(this.NothingFound));
            }

            return this.View(viewModelFromSearch);
        }

        public IActionResult NothingFound()
        {
            return this.View();
        }

        public IActionResult ById(int id)
        {
            try
            {
                var viewModel = this.winesService.GetWine<WineViewModel>(id);

                if (viewModel.IsApproved == false && !this.User.IsInRole("Administrator"))
                {
                    return this.NotFound();
                }

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.Redirect("/Wines");
            }
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new WinesListViewModel
            {
                PageNumber = id,
                ItemsCount = this.winesService.GetCount(),
                ItemsPerPage = this.itemsPerPage,
                Wines = this.winesService.GetApprovedOnly(id, this.itemsPerPage),
            };

            return this.View(viewModel);
        }
    }
}

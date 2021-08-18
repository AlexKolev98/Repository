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
    using Newtonsoft.Json;
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

        public IActionResult AllByX(string searchByInput, string searchBy, int pageNumberId = 1)
        {
            // IF tempData != null, this is being redirected by SearchController.
            var tempData = this.TempData["isComingFrom"];

            if (tempData != null)
            {
                searchByInput = JsonConvert.DeserializeObject<string>(this.TempData["searchByInput"].ToString());
                searchBy = JsonConvert.DeserializeObject<string>(this.TempData["searchBy"].ToString());
                var comingFrom = JsonConvert.DeserializeObject<string>(this.TempData["isComingFrom"].ToString());

                if (searchBy != null && searchByInput != null && comingFrom != null)
                {
                    var viewModel = new WinesListViewModel
                    {
                        PageNumber = pageNumberId,
                        ItemsPerPage = this.itemsPerPage,
                        ItemsCount = this.winesService.GetCount(),
                        Wines = this.winesService.GetAllByX(pageNumberId, this.itemsPerPage, searchByInput, searchBy, comingFrom),
                        SearchBy = searchBy,
                        SearchByInput = searchByInput,
                    };
                    return this.View(viewModel);
                }

                return this.Redirect("/Wines/" + nameof(this.NothingFound));
            }
            else if (searchByInput != null && searchBy != null)
            {
                var viewModel = new WinesListViewModel
                {
                    PageNumber = pageNumberId,
                    ItemsCount = this.winesService.GetCount(),
                    ItemsPerPage = this.itemsPerPage,
                    Wines = this.winesService.GetAllByX(pageNumberId, this.itemsPerPage, searchByInput, searchBy, this.isComingFrom),
                    SearchBy = searchBy,
                    SearchByInput = searchByInput,
                };
                if (viewModel.Wines == null || viewModel == null)
                {
                    this.Redirect("/Wines/" + nameof(this.NothingFound));
                }

                this.TempData["postViewModel"] = JsonConvert.SerializeObject(viewModel);
                return this.View(viewModel);
            }

            return this.Redirect("/Wines/" + nameof(this.NothingFound));
        }

        public IActionResult NothingFound()
        {
            return this.View();
        }

        public IActionResult ById(int id)
        {
            try
            {
                if (this.User.IsInRole("Administrator"))
                {
                    var viewModel = this.winesService.GetWineDespiteDeleted<WineViewModel>(id);
                    return this.View(viewModel);
                }
                else
                {
                    var viewModel = this.winesService.GetWine<WineViewModel>(id);

                    if (viewModel.IsApproved == false && !this.User.IsInRole("Administrator"))
                    {
                        return this.NotFound();
                    }

                    return this.View(viewModel);
                }
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

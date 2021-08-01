namespace TheGreatGrape.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TheGreatGrape.Data.Models;
    using TheGreatGrape.Services.Data;
    using TheGreatGrape.Services.Data.Create;
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

        public WinesController(
            IWinesService winesService,
            IGrapesService grapesService,
            ICategoriesService categoriesService,
            IWineriesService wineriesService,
            ICreateWineService createWineService,
            ICountriesService countriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.winesService = winesService;
            this.grapesService = grapesService;
            this.categoriesService = categoriesService;
            this.wineriesService = wineriesService;
            this.createWineService = createWineService;
            this.countriesService = countriesService;
            this.userManager = userManager;
        }

        public IActionResult Index(int id = 1)
        {
            const int itemsPerPage = 12;
            var viewModel = new WinesListViewModel
            {
                PageNumber = id,
                Wines = this.winesService.GetAll(id, itemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult Red()
        {
            var viewModel = this.winesService.GetRed();
            return this.View(viewModel);
        }

        public IActionResult White()
        {
            var viewModel = this.winesService.GetWhite();
            return this.View(viewModel);
        }

        public IActionResult Rosé()
        {
            var viewModel = this.winesService.GetRosé();
            return this.View(viewModel);
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
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAllAsKeyValuePairs();
                input.Wineries = this.wineriesService.GetAllAsKeyValuePairs();
                input.Grapes = this.grapesService.GetAllAsKeyValuePairs();
                input.Countries = this.countriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.createWineService.CreateAsync(input, user.Id);

            return this.Redirect("/");
        }
    }
}

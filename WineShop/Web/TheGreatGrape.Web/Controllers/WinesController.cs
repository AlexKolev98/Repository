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

        private readonly int itemsPerPage = 3;

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
            const int itemsPerPage = 3;
            var viewModel = new WinesListViewModel
            {
                PageNumber = id,
                ItemsCount = this.winesService.GetCount(),
                Wines = this.winesService.GetAll<WinesListViewModel>(id, itemsPerPage),
            };

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
            try
            {
                await this.createWineService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.Redirect("/");
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.winesService.GetWine<WineViewModel>(id);

            return this.View(viewModel);
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
                Wines = this.winesService.GetAll<WinesListViewModel>(id, this.itemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult AllByWinery(int wineryId, int id = 1)
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
                Wines = this.winesService.GetAllByWinery(id, this.itemsPerPage, wineryId),
            };

            return this.View(viewModel);
        }

        public IActionResult AllByGrape(int grapeId, int id = 1)
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
                Wines = this.winesService.GetAllByGrape(id, this.itemsPerPage, grapeId),
            };

            return this.View(viewModel);
        }

        public IActionResult AllBySweetness(string sweetness, int id = 1)
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
                Wines = this.winesService.GetAllBySweetness(id, this.itemsPerPage, sweetness),
            };

            return this.View(viewModel);
        }

        public IActionResult AllByColour(int categoryId, int id = 1)
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
                Wines = this.winesService.GetAllByCategory(id, this.itemsPerPage, categoryId),
            };

            return this.View(viewModel);
        }

        public IActionResult AllByCountry(int countryId, int id = 1)
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
                Wines = this.winesService.GetAllByCountry(id, this.itemsPerPage, countryId),
            };

            return this.View(viewModel);
        }

        public IActionResult AllByYear(int year, int id = 1)
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
                Wines = this.winesService.GetAllByYear(id, this.itemsPerPage, year),
            };

            return this.View(viewModel);
        }

        public IActionResult AllByVolume(int volume, int id = 1)
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
                Wines = this.winesService.GetAllByVolume(id, this.itemsPerPage, volume),
            };

            return this.View(viewModel);
        }

        public IActionResult AllByAlcohol(decimal alcohol, int id = 1)
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
                Wines = this.winesService.GetAllByAlcohol(id, this.itemsPerPage, alcohol),
            };

            return this.View(viewModel);
        }
    }
}

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
    using TheGreatGrape.Web.ViewModels.Wineries;
    using TheGreatGrape.Web.ViewModels.Wineries.Create;

    public class WineriesController : BaseController
    {
        private readonly ICreateWineryService createWineryService;
        private readonly IWineriesService wineriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        private readonly int itemsPerPage = 12;

        public WineriesController(
            ICreateWineryService createWineryService,
            IWineriesService wineriesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.createWineryService = createWineryService;
            this.wineriesService = wineriesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            var viewModel = this.wineriesService.GetAll();
            return this.View(viewModel);
        }

        public IActionResult All(int id = 1)
        {
            var viewModel = new WineriesListViewModel
            {
                PageNumber = id,
                ItemsCount = this.wineriesService.GetCount(),
                ItemsPerPage = this.itemsPerPage,
                Wineries = this.wineriesService.GetAll<WineriesListViewModel>(id, this.itemsPerPage),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateWineryInputModel();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateWineryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.createWineryService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.Redirect("/Wineries/All");
        }

        public IActionResult ById(int id)
        {
            try
            {
                if (this.User.IsInRole("Administrator"))
                {
                    var viewModel = this.wineriesService.GetWineryDespiteDeleted(id);
                    return this.View(viewModel);
                }
                else
                {
                    var viewModel = this.wineriesService.GetWinery(id);
                    if (viewModel == null)
                    {
                        return this.NotFound();
                    }

                    return this.View(viewModel);
                }
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.Redirect("/Wineries");
            }
        }
    }
}

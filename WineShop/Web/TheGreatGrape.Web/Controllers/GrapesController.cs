using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheGreatGrape.Data.Models;
using TheGreatGrape.Services.Data;
using TheGreatGrape.Services.Data.Create;
using TheGreatGrape.Web.ViewModels.Grapes;
using TheGreatGrape.Web.ViewModels.Grapes.Create;
using TheGreatGrape.Web.ViewModels.Wines.Create;

namespace TheGreatGrape.Web.Controllers
{
    public class GrapesController : BaseController
    {
        private readonly ICreateGrapeService createGrapeService;
        private readonly IGrapesService grapesService;
        private readonly UserManager<ApplicationUser> userManager;

        public GrapesController(
            ICreateGrapeService createGrapeService,
            IGrapesService grapesService,
            UserManager<ApplicationUser> userManager
            )
        {
            this.createGrapeService = createGrapeService;
            this.grapesService = grapesService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = this.grapesService.GetAll();
            return this.View(viewModel);
        }

        public IActionResult All(int id = 1)
        {
            int itemsPerPage = 3;
            var viewModel = new GrapesListViewModel
            {
                PageNumber = id,
                ItemsCount = this.grapesService.GetCount(),
                ItemsPerPage = itemsPerPage,
                Grapes = this.grapesService.GetAll<GrapesListViewModel>(id, itemsPerPage),
            };
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateGrapeInputModel();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGrapeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.createGrapeService.CreateAsync(input, user.Id);
            return this.Redirect("/");
        }
    }
}

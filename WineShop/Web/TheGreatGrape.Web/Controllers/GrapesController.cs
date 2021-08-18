namespace TheGreatGrape.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TheGreatGrape.Data.Models;
    using TheGreatGrape.Services.Data;
    using TheGreatGrape.Services.Data.Create;
    using TheGreatGrape.Web.ViewModels.Grapes;
    using TheGreatGrape.Web.ViewModels.Grapes.Create;
    using TheGreatGrape.Web.ViewModels.Wines;

    public class GrapesController : BaseController
    {
        private readonly ICreateGrapeService createGrapeService;
        private readonly IGrapesService grapesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWinesService winesService;

        public GrapesController(
            ICreateGrapeService createGrapeService,
            IGrapesService grapesService,
            UserManager<ApplicationUser> userManager,
            IWinesService winesService)
        {
            this.createGrapeService = createGrapeService;
            this.grapesService = grapesService;
            this.userManager = userManager;
            this.winesService = winesService;
        }

        public IActionResult Index()
        {
            var viewModel = this.grapesService.GetAll();
            return this.View(viewModel);
        }

        public IActionResult All(int id = 1)
        {
            int itemsPerPage = 32;
            var grapes = this.grapesService.GetAll<GrapesListViewModel>(id, itemsPerPage);
            foreach (var item in grapes)
            {
                item.WinesCount = this.grapesService.GetWinesCount(item.Id);
            }

            var viewModel = new GrapesListViewModel
            {
                PageNumber = id,
                ItemsCount = this.grapesService.GetCount(),
                ItemsPerPage = itemsPerPage,
                Grapes = grapes.OrderByDescending(x => x.WinesCount).ThenBy(x => x.Name),
            };

            var count = viewModel.WinesCount;

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
            if (this.User.IsInRole("Administrator"))
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(input);
                }

                try
                {
                    var count = this.grapesService.GetCount();
                    if (!this.ModelState.IsValid)
                    {
                        return this.View(input);
                    }

                    var user = await this.userManager.GetUserAsync(this.User);
                    await this.createGrapeService.CreateAsync(input, user.Id);
                    if (this.grapesService.GetCount() == count)
                    {
                        return this.Redirect("/Grapes/Create");
                    }

                    return this.Redirect("/Wines/Create");

                }
                catch (Exception ex)
                {

                    this.ModelState.AddModelError(string.Empty, ex.Message);
                    return this.View(input);
                }
            }

            return this.Redirect("/");
        }
    }
}

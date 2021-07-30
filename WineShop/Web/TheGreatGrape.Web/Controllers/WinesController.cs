namespace TheGreatGrape.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TheGreatGrape.Services.Data;
    using TheGreatGrape.Services.Data.Create;
    using TheGreatGrape.Web.ViewModels.Wines.Create;

    public class WinesController : BaseController
    {
        private readonly IWinesService winesService;
        private readonly IGrapesService grapesService;
        private readonly ICategoriesService categoriesService;
        private readonly IWineriesService wineriesService;
        private readonly ICreateWineService createWineService;

        public WinesController(
            IWinesService winesService,
            IGrapesService grapesService,
            ICategoriesService categoriesService,
            IWineriesService wineriesService,
            ICreateWineService createWineService)
        {
            this.winesService = winesService;
            this.grapesService = grapesService;
            this.categoriesService = categoriesService;
            this.wineriesService = wineriesService;
            this.createWineService = createWineService;
        }

        public IActionResult Index()
        {
            var viewModel = this.winesService.GetAll();
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

        public IActionResult Create()
        {
            var viewModel = new CreateWineInputModel();
            viewModel.Categories = this.categoriesService.GetAllAsKeyValuePairs();
            viewModel.Wineries = this.wineriesService.GetAllAsKeyValuePairs();
            viewModel.Grapes = this.grapesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWineInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = this.categoriesService.GetAllAsKeyValuePairs();
                input.Wineries = this.wineriesService.GetAllAsKeyValuePairs();
                input.Grapes = this.grapesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            return this.Redirect("/");
        }
    }
}

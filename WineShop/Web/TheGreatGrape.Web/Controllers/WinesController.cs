using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheGreatGrape.Services.Data;

namespace TheGreatGrape.Web.Controllers
{
    public class WinesController : BaseController
    {
        private readonly IGetWinesService winesService;

        public WinesController(IGetWinesService winesService)
        {
            this.winesService = winesService;
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
    }
}

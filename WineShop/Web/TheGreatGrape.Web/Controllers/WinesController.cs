using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheGreatGrape.Web.Controllers
{
    public class WinesController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}

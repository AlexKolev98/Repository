namespace TheGreatGrape.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TheGreatGrape.Common;
    using TheGreatGrape.Data.Models;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Data;
    using TheGreatGrape.Web.ViewModels.Carts;

    public class CartController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWinesService winesService;

        public CartController(
            UserManager<ApplicationUser> userManager,
            IWinesService winesService)
        {
            this.userManager = userManager;
            this.winesService = winesService;
        }

        // Cart is not implemented!!!

        [Authorize]
        public IActionResult Index()
        {
            // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // if (this.User.Identity.IsAuthenticated)
            // {
            //     var shoppingCartProducts = this.shoppingCartService.GetAllShoppingCartProducts(userId);
            //
            //     if (shoppingCartProducts.Count() == 0)
            //     {
            //         return this.Redirect("/");
            //     }
            //
            //     var viewModel = shoppingCartProducts.Select(x => new ShoppingCartViewModel
            //     {
            //         Id = x.WineId,
            //         ImageUrl = x.Wine.ImageUrl,
            //         Name = x.Wine.Name,
            //         Price = x.Wine.Price,
            //         Quantity = x.Quantity,
            //         TotalPrice = x.Quantity * x.Wine.Price,
            //     });
            //
            //     return this.View(viewModel);
            // }
            //
            // var shoppingCartSession = SessionHelper.GetObjectFromJson<List<ShoppingCartViewModel>>(this.HttpContext.Session, TheGreatGrape.Web.Common.GlobalConstants.SESSION_SHOPPING_CART_KEY);
            // if (shoppingCartSession == null || shoppingCartSession.Count == 0)
            // {
            //     return this.Redirect("/");
            // }
            //
            // return this.View(shoppingCartSession);

            return this.View();
        }

      // public IActionResult Add()
      // {
      //     var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
      //
      //     var viewModel = new AddItemInputModel();
      //     return this.View(viewModel);
      // }
      //
      // [HttpPost]
      // public async Task<IActionResult> Add(int itemId, AddItemInputModel inputQuantity)
      // {
      //     var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
      //     if (this.User.Identity.IsAuthenticated)
      //     {
      //         await this.shoppingCartService.AddToCart(itemId, userId, inputQuantity.Quantity);
      //     }
      //     else
      //     {
      //         List<ShoppingCartViewModel> shoppingCartSession = SessionHelper.GetObjectFromJson<List<ShoppingCartViewModel>>(this.HttpContext.Session, TheGreatGrape.Web.Common.GlobalConstants.SESSION_SHOPPING_CART_KEY);
      //
      //         if (shoppingCartSession == null)
      //         {
      //             shoppingCartSession = new List<ShoppingCartViewModel>();
      //         }
      //
      //         if (!shoppingCartSession.Any(x => x.Id == itemId))
      //         {
      //             var product = this.winesService.GetWine<ShoppingCartViewModel>(itemId);
      //             product.Quantity = inputQuantity.Quantity;
      //             product.TotalPrice = product.Quantity * product.Price;
      //
      //             shoppingCartSession.Add(product);
      //
      //             SessionHelper.SetObjectAsJson(this.HttpContext.Session, TheGreatGrape.Web.Common.GlobalConstants.SESSION_SHOPPING_CART_KEY, shoppingCartSession);
      //         }
      //     }
      //
      //     return this.Redirect("/Cart");
      // }
    }
}

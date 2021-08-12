namespace TheGreatGrape.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Data.Contracts;
    using TheGreatGrape.Services.Mapping;
    using TheGreatGrape.Web.ViewModels.Carts;

    public class ShoppingCartService : IShoppingCartService
    {
        private const int DefaultQuantity = 1;

        private readonly IRepository<CartWine> cartItemsRepository;
        private readonly IRepository<Wine> winesRepository;
        private readonly IRepository<ShoppingCart> shoppingCartRepository;
        private readonly IWinesService winesService;

        public ShoppingCartService(
            IRepository<CartWine> cartItemsRepository,
            IDeletableEntityRepository<Wine> winesRepository,
            IDeletableEntityRepository<ShoppingCart> shoppingCartRepository,
            IWinesService winesService)
        {
            this.cartItemsRepository = cartItemsRepository;
            this.winesRepository = winesRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.winesService = winesService;
        }

        public async Task AddToCart(int itemId, string userId, int quantity)
        {
            if (this.shoppingCartRepository.All().Any(x => x.UserId == userId))
            {
            }
            var itemToAdd = this.winesRepository.All().FirstOrDefault(x => x.Id == itemId);

            if (itemToAdd == null)
            {
                return;
            }

            var shoppingCartProduct = this.GetCartWine(itemId, this.GetCartId(userId));

            if (shoppingCartProduct != null)
            {
                return;
            }

            shoppingCartProduct = new CartWine
            {
                WineId = itemId,
                Wine = itemToAdd,
                ShoppingCartId = this.GetCartId(userId),
                Quantity = quantity,
            };

            await this.cartItemsRepository.AddAsync(shoppingCartProduct);

            await this.shoppingCartRepository.SaveChangesAsync();
        }

        public async Task Dispose(string userId)
        {
            this.shoppingCartRepository.All().FirstOrDefault(x => x.UserId == userId).IsDeleted = true;
            await this.shoppingCartRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetCartItems<T>(string userId)
        {
            var items = this.shoppingCartRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .To<T>()
                .ToList();

            return items;
        }

        public IEnumerable<CartWine> GetAllShoppingCartProducts(string userId)
        {
            //Check in controller!!!
          // var user = this.userService.GetUserByUsername(username);
          //
          // if (user == null)
          // {
          //     return null;
          // }

            return this.cartItemsRepository.All().Include(x => x.Wine)
                                               .ThenInclude(x => x.Image)
                                               .Include(x => x.ShoppingCart)
                                               .Where(x => x.ShoppingCart.User.Id == userId).ToList();
        }

        public int GetCartId(string userId)
        {
            var rep = this.shoppingCartRepository.All().FirstOrDefault(x => x.UserId == userId);
            var id = rep.Id;
            return id;
        }

        private CartWine GetCartWine(int itemId, int shoppingCartId)
        {
            if (shoppingCartId != 0)
            {
                return this.shoppingCartRepository.All().FirstOrDefault(x => x.Id == shoppingCartId).CartWines.FirstOrDefault(w => w.Wine.Id == itemId && w.ShoppingCartId == shoppingCartId);
            }

            return null;
        }
    }
}

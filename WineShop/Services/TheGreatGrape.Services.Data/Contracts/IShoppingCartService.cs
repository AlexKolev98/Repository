namespace TheGreatGrape.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using TheGreatGrape.Web.ViewModels.Carts;

    public interface IShoppingCartService
    {
        public IEnumerable<T> GetCartItems<T>(string userId);

        public Task Dispose(string userId);

        public Task AddToCart(int itemId, string userId, int quantity);

        int GetCartId(string userId);

        public IEnumerable<CartWine> GetAllShoppingCartProducts(string userId);
    }
}

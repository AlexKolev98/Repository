using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGreatGrape.Data.Common.Models;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Data.Models.TheGreatGrape.Models;

namespace TheGreatGrape.Services.Data.Create
{
    public class CreateShoppingCartService : ICreateShoppingCartService
    {
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartRepository;

        public CreateShoppingCartService(IDeletableEntityRepository<ShoppingCart> shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task CreateAsync(string userId)
        {
            var newCart = new ShoppingCart
            {
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
            };
            await this.shoppingCartRepository.AddAsync(newCart);
            await this.shoppingCartRepository.SaveChangesAsync();
        }
    }
}

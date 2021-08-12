using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheGreatGrape.Data.Models.TheGreatGrape.Models;

namespace TheGreatGrape.Services.Data.Create
{
    public interface ICreateShoppingCartService
    {
        public Task CreateAsync(string userId);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TheGreatGrape.Data.Models.TheGreatGrape.Models;
using TheGreatGrape.Data.Models.WineShop;
using TheGreatGrape.Services.Mapping;

namespace TheGreatGrape.Web.ViewModels.Carts
{
    public class ShoppingCartViewModel : IMapFrom<Wine>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string ImageUrl { get; set; }
    }
}

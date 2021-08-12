using System;
using System.Collections.Generic;
using System.Text;
using TheGreatGrape.Data.Models.TheGreatGrape.Models;
using TheGreatGrape.Data.Models.WineShop;
using TheGreatGrape.Services.Mapping;

namespace TheGreatGrape.Web.ViewModels.Carts
{
    public class CartWineViewModel : IMapFrom<CartWine>
    {
        public int WineId { get; set; }

        public string WineName { get; set; }

        public string WineImageUrl { get; set; }

        public int Quantity{ get; set; }
    }
}

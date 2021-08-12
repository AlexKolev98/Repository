using System;
using System.Collections.Generic;
using System.Text;
using TheGreatGrape.Data.Common.Models;

namespace TheGreatGrape.Data.Models.TheGreatGrape.Models
{
    public class ShoppingCart : BaseDeletableModel<int>
    {
        public ShoppingCart()
        {
            this.CartWines = new HashSet<CartWine>();
        }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public ICollection<CartWine> CartWines { get; set; }
    }
}

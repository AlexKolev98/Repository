namespace TheGreatGrape.Web.ViewModels.Carts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AddItemInputModel
    {
        public int Quantity { get; set; }

        public ShoppingCartViewModel ShoppingCart { get; set; }

    }
}

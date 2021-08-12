namespace TheGreatGrape.Data.Models.TheGreatGrape.Models
{
    using global::TheGreatGrape.Data.Common.Models;
    using global::TheGreatGrape.Data.Models.WineShop;

    public class CartWine : BaseModel<string>
    {
        public int ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public int WineId { get; set; }

        public virtual Wine Wine { get; set; }

        public int Quantity { get; set; }
    }
}

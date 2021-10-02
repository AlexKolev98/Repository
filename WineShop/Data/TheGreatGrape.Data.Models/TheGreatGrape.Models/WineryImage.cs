namespace TheGreatGrape.Data.Models.WineShop
{
    using global::TheGreatGrape.Data.Models.TheGreatGrape.Models.Base;

    public class WineryImage : Image
    {
        public int WineryId { get; set; }

        public Winery Winery { get; set; }
    }
}

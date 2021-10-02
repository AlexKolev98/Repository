namespace TheGreatGrape.Data.Models.WineShop
{
    using global::TheGreatGrape.Data.Models.TheGreatGrape.Models.Base;

    public class WineImage : Image
    {
        public int WineId { get; set; }

        public Wine Wine { get; set; }
    }
}

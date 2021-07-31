namespace TheGreatGrape.Data.Models.WineShop
{
    using global::TheGreatGrape.Data.Common.Models;

    public class WineGrape : BaseModel<int>
    {
        public int WineId { get; set; }

        public int GrapeId { get; set; }

        public Grape Grape { get; set; }

        public Wine Wine { get; set; }
    }
}
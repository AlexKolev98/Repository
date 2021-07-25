namespace TheGreatGrape.Data.Models.WineShop
{
    using System.Collections.Generic;

    public class WineGrape
    {
        public int Id { get; set; }

        public int WineId { get; set; }

        public int GrapeId { get; set; }

        public virtual Wine Wine { get; set; }

        public virtual Grape Grape { get; set; }
    }
}

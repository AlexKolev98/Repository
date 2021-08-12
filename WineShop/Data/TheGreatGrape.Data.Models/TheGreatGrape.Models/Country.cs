namespace TheGreatGrape.Data.Models.WineShop
{
    using System.Collections;
    using System.Collections.Generic;

    using global::TheGreatGrape.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Wines = new HashSet<Wine>();
        }

        public string Name { get; set; }

        public ICollection<Wine> Wines { get; set; }
    }
}

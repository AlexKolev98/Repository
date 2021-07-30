namespace TheGreatGrape.Data.Models.WineShop
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using global::TheGreatGrape.Data.Common.Models;
    using global::TheGreatGrape.Data.Models.TheGreatGrape.Models;

    public class Wine : BaseDeletableModel<int>
    {
        public Wine()
        {
            this.WineGrapes = new HashSet<WineGrape>();
        }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Volume { get; set; }

        public int Year { get; set; }

        public int WineryId { get; set; }

        public Winery Winery { get; set; }

        public virtual ICollection<WineGrape> WineGrapes { get; set; }

        public string Description { get; set; }

        public virtual WineImage WineImage { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}

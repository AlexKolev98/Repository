using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TheGreatGrape.Data.Common.Models;
using TheGreatGrape.Data.Models.WineShop;

public class Grape : BaseDeletableModel<int>
    {
        public Grape()
        {
            this.WineGrapes = new HashSet<WineGrape>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<WineGrape> WineGrapes { get; set; }
}
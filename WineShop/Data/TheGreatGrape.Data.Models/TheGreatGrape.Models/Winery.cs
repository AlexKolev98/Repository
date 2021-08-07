namespace TheGreatGrape.Data.Models.WineShop
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::TheGreatGrape.Data.Common.Models;

    public class Winery : BaseDeletableModel<int>
    {
        public Winery()
        {
            this.Wines = new HashSet<Wine>();
            this.WineryImages = new HashSet<WineryImage>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<Wine> Wines { get; set; }

        [Required]
        public string Description { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<WineryImage> WineryImages { get; set; }
    }
}

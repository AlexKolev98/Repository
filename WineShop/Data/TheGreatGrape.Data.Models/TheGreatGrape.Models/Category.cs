namespace TheGreatGrape.Data.Models.TheGreatGrape.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using global::TheGreatGrape.Data.Common.Models;
    using global::TheGreatGrape.Data.Models.WineShop;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Wines = new HashSet<Wine>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<Wine> Wines { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}

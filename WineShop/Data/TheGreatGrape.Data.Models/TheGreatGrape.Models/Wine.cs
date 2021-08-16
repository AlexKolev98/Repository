namespace TheGreatGrape.Data.Models.WineShop
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using global::TheGreatGrape.Data.Common.Models;
    using global::TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using global::TheGreatGrape.Data.Models.TheGreatGrape.Models.Enums;

    public class Wine : BaseDeletableModel<int>
    {
        public Wine()
        {
            this.Grapes = new HashSet<WineGrape>();
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Alcohol { get; set; }

        public int Volume { get; set; }

        public int Year { get; set; }

        public int WineryId { get; set; }

        public Winery Winery { get; set; }

        public ICollection<WineGrape> Grapes { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public string Description { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public string AddedByUserId { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string ImageUrl { get; set; }

        public virtual WineImage Image { get; set; }

        public Country Country { get; set; }

        public int CountryId { get; set; }

        public SweetnessEnum Sweetness { get; set; }

        public bool IsApproved { get; set; }
    }
}

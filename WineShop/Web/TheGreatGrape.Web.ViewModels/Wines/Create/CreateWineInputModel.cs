namespace TheGreatGrape.Web.ViewModels.Wines.Create
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models.Enums;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Web.ViewModels.Grapes.Create;

    public class CreateWineInputModel
    {
        public CreateWineInputModel()
        {
            this.GrapesIdCollection = new HashSet<int>();
        }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Display(Name = "Price (BGN)")]
        public decimal Price { get; set; }

        [Display(Name = "Alcohol (%)")]
        public decimal Alcohol { get; set; }

        [Range(250, 50000)]
        [Display(Name = "Volume (ml)")]
        public int Volume { get; set; }

        [Range(1960, 2021)]
        public int Year { get; set; }

        [MinLength(20)]
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public SweetnessEnum Sweetness{ get; set; }

        [Display(Name ="Country")]
        public int CountryId{ get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Grapes")]
        public int GrapeId { get; set; }

        [Display(Name = "Winery")]
        public int WineryId { get; set; }

        public ICollection<int> GrapesIdCollection { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Countries { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Wineries { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Grapes { get; set; }
    }
}

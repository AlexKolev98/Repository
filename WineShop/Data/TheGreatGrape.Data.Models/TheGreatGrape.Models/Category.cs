using System;
using System.Collections.Generic;
using System.Text;
using TheGreatGrape.Data.Common.Models;
using TheGreatGrape.Data.Models.WineShop;

namespace TheGreatGrape.Data.Models.TheGreatGrape.Models
{
    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Wines = new HashSet<Wine>();
        }

        public string Name { get; set; }

        public ICollection<Wine> Wines { get; set; }
    }
}

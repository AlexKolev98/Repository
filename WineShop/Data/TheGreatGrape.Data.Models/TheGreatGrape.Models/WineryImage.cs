namespace TheGreatGrape.Data.Models.WineShop
{
    using global::TheGreatGrape.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WineryImage : BaseDeletableModel<int>
    {
        public int WineryId { get; set; }

        public Winery Winery { get; set; }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}

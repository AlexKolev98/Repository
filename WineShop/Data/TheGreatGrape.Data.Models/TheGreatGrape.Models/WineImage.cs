namespace TheGreatGrape.Data.Models.WineShop
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using global::TheGreatGrape.Data.Common.Models;

    public class WineImage : BaseDeletableModel<int>
    {
        public int WineId { get; set; }

        public Wine Wine { get; set; }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}

namespace TheGreatGrape.Data.Models.WineShop
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using global::TheGreatGrape.Data.Common.Models;

    public class Image : BaseModel<int>
    {
        public Image()
        {
            this.WineImages = new HashSet<WineImage>();
            this.WineryImages = new HashSet<WineryImage>();
        }

        public virtual ICollection<WineImage> WineImages { get; set; }

        public virtual ICollection<WineryImage> WineryImages { get; set; }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}

namespace TheGreatGrape.Data.Models.WineShop
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using global::TheGreatGrape.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public Image()
        {
            this.WineImages = new HashSet<WineImage>();
            this.WineryImages = new HashSet<WineryImage>();
        }

        public int WineImageId { get; set; }

        public int WineryImageId { get; set; }

        public virtual ICollection<WineImage> WineImages { get; set; }

        public virtual ICollection<WineryImage> WineryImages { get; set; }
    }
}

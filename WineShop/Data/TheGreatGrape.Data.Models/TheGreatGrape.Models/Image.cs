namespace TheGreatGrape.Data.Models.WineShop
{
    using global::TheGreatGrape.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Image : BaseModel<int>
    {
        public Image()
        {
            this.WineImages = new HashSet<WineImage>();
            this.WineryImages = new HashSet<WineryImage>();
        }

        public virtual ICollection<WineImage> WineImages { get; set; }

        public virtual ICollection<WineryImage> WineryImages { get; set; }
    }
}

namespace TheGreatGrape.Data.Models.WineShop
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WineryImage
    {
        public int Id { get; set; }

        public int ImageId { get; set; }

        public int WineryId { get; set; }

        public virtual Winery Winery { get; set; }

        public virtual Image Image { get; set; }
    }
}

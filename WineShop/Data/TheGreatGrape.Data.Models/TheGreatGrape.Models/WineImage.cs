namespace TheGreatGrape.Data.Models.WineShop
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using global::TheGreatGrape.Data.Common.Models;

    public class WineImage
    {
        public int Id { get; set; }

        public int ImageId { get; set; }

        public int WineId { get; set; }

        public virtual Wine Wine { get; set; }

        public virtual Image Image { get; set; }
    }
}

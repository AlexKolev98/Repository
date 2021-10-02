namespace TheGreatGrape.Web.ViewModels.Wines
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using TheGreatGrape.Data.Models.WineShop;

    public class GetWinesViewModel
    {
        public GetWinesViewModel()
        {
            this.Wines = new HashSet<Wine>();
        }

        public ICollection<Wine> Wines { get; set; }
    }
}

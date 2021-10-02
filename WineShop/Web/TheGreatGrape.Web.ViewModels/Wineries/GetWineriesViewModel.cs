namespace TheGreatGrape.Web.ViewModels.Wineries
{
    using System.Collections.Generic;

    using TheGreatGrape.Data.Models.WineShop;

    public class GetWineriesViewModel
    {
        public GetWineriesViewModel()
        {
            this.Wineries = new HashSet<Winery>();
        }

        public ICollection<Winery> Wineries { get; set; }
    }
}

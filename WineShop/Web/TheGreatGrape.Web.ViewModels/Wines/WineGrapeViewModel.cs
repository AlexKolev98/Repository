namespace TheGreatGrape.Web.ViewModels.Wines
{

    using AutoMapper;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Mapping;

    public class WineGrapeViewModel : IMapFrom<WineGrape>
    {
        public int GrapeId { get; set; }

        public string GrapeName { get; set; }
    }
}

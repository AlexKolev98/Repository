namespace TheGreatGrape.Web.ViewModels.Wines
{

    using AutoMapper;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Mapping;

    public class WineGrapeViewModel : IMapFrom<WineGrape>
    {
        public int GrapeId { get; set; }

        public string GrapeName { get; set; }

        public int WineId { get; set; }

        public string WineName{ get; set; }

        public bool IsApproved { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WineGrape, WineGrapeViewModel>()
                .ForMember(x => x.IsApproved, opt => opt.MapFrom(x => x.Wine.IsApproved));
        }
    }
}

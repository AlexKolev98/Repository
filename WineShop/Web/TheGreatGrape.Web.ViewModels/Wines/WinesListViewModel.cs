namespace TheGreatGrape.Web.ViewModels.Wines
{
    using System.Collections.Generic;

    using AutoMapper;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models.Enums;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Mapping;

    public class WinesListViewModel : ItemListViewModel, IMapFrom<Wine>, IHaveCustomMappings
    {
        public IEnumerable<WinesListViewModel> Wines { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Alcohol { get; set; }

        public int Volume { get; set; }

        public int WineryId { get; set; }

        public int CountryId { get; set; }

        public int GrapeId { get; set; }

        public int Year { get; set; }

        public int CategoryId { get; set; }

        public SweetnessEnum Sweetness { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Wine, WinesListViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Image.RemoteImageUrl != null ?
                x.Image.RemoteImageUrl
                : "/images/wines/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}

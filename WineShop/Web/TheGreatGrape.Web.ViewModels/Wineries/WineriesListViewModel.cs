namespace TheGreatGrape.Web.ViewModels.Wineries
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Mapping;

    public class WineriesListViewModel : ItemListViewModel, IMapFrom<Winery>, IHaveCustomMappings
    {
        public IEnumerable<WineriesListViewModel> Wineries { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Winery, WineriesListViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.WineryImages.FirstOrDefault().RemoteImageUrl != null ?
                x.WineryImages.FirstOrDefault().RemoteImageUrl
                : "/images/wineries/" + x.WineryImages.FirstOrDefault().Id + "." + x.WineryImages.FirstOrDefault().Extension));
        }
    }
}

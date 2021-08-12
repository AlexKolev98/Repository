namespace TheGreatGrape.Web.ViewModels.Wineries
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Mapping;
    using TheGreatGrape.Web.ViewModels.Wines;

    public class WineryViewModel : SingleItemViewModel, IMapFrom<Winery>, IHaveCustomMappings
    {
        public WineryViewModel()
        {
            this.Wines = new HashSet<WineViewModel>();
        }

        public IEnumerable<WineViewModel> Wines { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<WineryImageViewModel> WineryImages { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Winery, WineryViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.WineryImages.FirstOrDefault().RemoteImageUrl != null ?
                x.WineryImages.FirstOrDefault().RemoteImageUrl
                : "/images/wineries/" + x.WineryImages.FirstOrDefault().Id + "." + x.WineryImages.FirstOrDefault().Extension));
        }
    }
}

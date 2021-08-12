namespace TheGreatGrape.Web.ViewModels
{
    using AutoMapper;

    using TheGreatGrape.Data.Models.TheGreatGrape.Models.Base;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Mapping;

    public class WineryImageViewModel : IMapFrom<Image>, IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<WineryImage, WineryImageViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.RemoteImageUrl != null ?
                x.RemoteImageUrl
                : "/images/wineries/" + x.Id + "." + x.Extension));
        }
    }
}

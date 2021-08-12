namespace TheGreatGrape.Web.ViewModels.Wines
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models.Enums;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Mapping;

    public class WineViewModel : SingleItemViewModel, IMapFrom<Wine>, IHaveCustomMappings
    {
        public WineViewModel()
        {
            this.Grapes = new HashSet<WineGrapeViewModel>();
        }

        public decimal Price { get; set; }

        public decimal Alcohol { get; set; }

        public int Volume { get; set; }

        public int Year { get; set; }

        public int WineryId { get; set; }

        public string WineryName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<WineGrapeViewModel> Grapes { get; set; }

        public string ImageUrl { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public SweetnessEnum Sweetness { get; set; }

        public double AverageVote{ get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {

            configuration.CreateMap<Wine, WineViewModel>()
                .ForMember(x => x.Price, opt => opt.MapFrom(x => x.Price));

            configuration.CreateMap<Wine, WineViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Image.RemoteImageUrl != null ?
                x.Image.RemoteImageUrl
                : "/images/wines/" + x.Image.Id + "." + x.Image.Extension))
                .ForMember(x => x.AverageVote, opt => opt.MapFrom(x => x.Votes.Average(v => v.Value)));
        }
    }
}

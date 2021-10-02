using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGreatGrape.Data.Models.WineShop;
using TheGreatGrape.Services.Mapping;

namespace TheGreatGrape.Web.ViewModels.Grapes
{
    public class GrapeWineViewModel : IMapFrom<WineGrape>
    {
        public int GrapeId { get; set; }

        public string GrapeName { get; set; }

        public int WineId { get; set; }

        public string WineName { get; set; }

        public bool IsApproved { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Wine, GrapeWineViewModel>()
                .ForMember(x => x.IsApproved, opt => opt.MapFrom(x => x.IsApproved))
                .ForMember(x => x.GrapeId, opt => opt.MapFrom(x => x.Grapes.FirstOrDefault().Grape.Id))
                .ForMember(x => x.GrapeName, opt => opt.MapFrom(x => x.Grapes.FirstOrDefault().Grape.Name))
                .ForMember(x => x.WineId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.WineName, opt => opt.MapFrom(x => x.Name));
        }
    }
}

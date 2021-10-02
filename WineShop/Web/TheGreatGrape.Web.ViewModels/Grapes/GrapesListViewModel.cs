namespace TheGreatGrape.Web.ViewModels.Grapes
{
    using AutoMapper;
    using System.Collections.Generic;
    using System.Linq;

    using TheGreatGrape.Services.Mapping;
    using TheGreatGrape.Web.ViewModels.Wines;

    public class GrapesListViewModel : ItemListViewModel, IMapFrom<Grape>
    {
        public IEnumerable<GrapesListViewModel> Grapes { get; set; }

        public int WinesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Grape, GrapesListViewModel>()
                .ForMember(x => x.WinesCount, opt => opt.MapFrom(x => x.Wines.Count(x => x.Wine.IsApproved == true)));
        }
    }
}

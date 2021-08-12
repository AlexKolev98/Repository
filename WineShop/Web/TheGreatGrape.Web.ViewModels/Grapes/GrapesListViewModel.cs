namespace TheGreatGrape.Web.ViewModels.Grapes
{
    using System.Collections.Generic;

    using TheGreatGrape.Services.Mapping;

    public class GrapesListViewModel : ItemListViewModel, IMapFrom<Grape>
    {
        public IEnumerable<GrapesListViewModel> Grapes { get; set; }
    }
}

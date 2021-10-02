using System;
using System.Collections.Generic;
using System.Text;

namespace TheGreatGrape.Web.ViewModels.Grapes
{
    public class GetGrapesViewModel
    {
        public GetGrapesViewModel()
        {
            this.Grapes = new HashSet<Grape>();
        }

        public ICollection<Grape> Grapes { get; set; }
    }
}

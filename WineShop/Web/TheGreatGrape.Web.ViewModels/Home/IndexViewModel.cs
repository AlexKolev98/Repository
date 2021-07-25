using System;
using System.Collections.Generic;
using System.Text;
using TheGreatGrape.Data.Models.TheGreatGrape.Models;

namespace TheGreatGrape.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Categories = new HashSet<Category>();
        }

        public ICollection<Category> Categories { get; set; }
    }
}

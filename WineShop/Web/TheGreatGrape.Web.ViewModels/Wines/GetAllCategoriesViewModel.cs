namespace TheGreatGrape.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TheGreatGrape.Data.Models.TheGreatGrape.Models;

    public class GetAllCategoriesViewModel
    {
        public GetAllCategoriesViewModel()
        {
            this.Categories = new HashSet<Category>();
        }

        public ICollection<Category> Categories { get; set; }
    }
}

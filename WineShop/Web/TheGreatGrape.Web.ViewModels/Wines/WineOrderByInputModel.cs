using System;
using System.Collections.Generic;
using System.Text;

namespace TheGreatGrape.Web.ViewModels.Wines
{
    public class WineOrderByInputModel
    {
        public WineOrderByInputModel()
        {
            this.OrderByItems = new List<OrderByViewModel>
            {
                new OrderByViewModel{Text = "Alphabetically", Value = "Alphabetically"},
                new OrderByViewModel{Text = "New", Value = "New"},
                new OrderByViewModel{Text = "Old", Value = "Old"},
                new OrderByViewModel{Text = "Price", Value = "Price"},
                new OrderByViewModel{Text = "Alcohol", Value = "Alcohol"},
                new OrderByViewModel{Text = "Volume", Value = "Volume"},
            };
        }

        public IEnumerable<OrderByViewModel> OrderByItems { get; set; }

        public string Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGreatGrape.Data.Models.WineShop;
using TheGreatGrape.Services.Mapping;

namespace TheGreatGrape.Services.Data.Tests.Models
{
    public class WineTestModel : IMapFrom<Wine>
    {
        public string Id { get; set; }
    }
}

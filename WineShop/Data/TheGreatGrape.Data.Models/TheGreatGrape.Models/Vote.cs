using System;
using System.Collections.Generic;
using System.Text;
using TheGreatGrape.Data.Common.Models;
using TheGreatGrape.Data.Models.WineShop;

namespace TheGreatGrape.Data.Models.TheGreatGrape.Models
{
    public class Vote : BaseModel<int>
    {
        public int WineId { get; set; }

        public virtual Wine Wine { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }

    }
}

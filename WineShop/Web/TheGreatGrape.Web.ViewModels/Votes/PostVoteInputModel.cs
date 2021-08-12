using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheGreatGrape.Web.ViewModels.Votes
{
    public class PostVoteInputModel
    {
        [Range(1, 5)]
        public byte Value { get; set; }

        public int WineId { get; set; }
    }
}

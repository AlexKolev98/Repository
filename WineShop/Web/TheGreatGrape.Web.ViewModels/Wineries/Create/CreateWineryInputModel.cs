namespace TheGreatGrape.Web.ViewModels.Wineries.Create
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateWineryInputModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(30)]
        public string Description { get; set; }

        [Required]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}

namespace TheGreatGrape.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using TheGreatGrape.Data.Models;

    public class SingleItemViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [MinLength(20)]
        public string Description { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}

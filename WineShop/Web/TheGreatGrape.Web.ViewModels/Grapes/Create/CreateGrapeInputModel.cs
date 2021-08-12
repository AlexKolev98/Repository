namespace TheGreatGrape.Web.ViewModels.Grapes.Create
{
    using System.ComponentModel.DataAnnotations;

    public class CreateGrapeInputModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}

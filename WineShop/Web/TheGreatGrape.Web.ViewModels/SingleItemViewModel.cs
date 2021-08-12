namespace TheGreatGrape.Web.ViewModels
{

    using TheGreatGrape.Data.Models;

    public class SingleItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}

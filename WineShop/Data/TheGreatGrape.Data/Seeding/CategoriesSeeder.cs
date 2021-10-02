namespace TheGreatGrape.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheGreatGrape.Data.Models.TheGreatGrape.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "White" });
            await dbContext.Categories.AddAsync(new Category { Name = "Red" });
            await dbContext.Categories.AddAsync(new Category { Name = "Rosé" });
        }
    }
}

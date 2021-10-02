namespace TheGreatGrape.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheGreatGrape.Data.Models.WineShop;

    public class CountriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Countries.Any())
            {
                return;
            }

            await dbContext.Countries.AddAsync(new Country { Name = "Bulgaria" });
            await dbContext.Countries.AddAsync(new Country { Name = "Italy" });
            await dbContext.Countries.AddAsync(new Country { Name = "France" });
        }
    }
}

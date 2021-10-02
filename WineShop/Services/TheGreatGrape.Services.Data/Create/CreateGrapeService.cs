namespace TheGreatGrape.Services.Data.Create
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheGreatGrape.Data.Common.Models;
    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Web.ViewModels.Grapes.Create;

    public class CreateGrapeService : ICreateGrapeService
    {
        private readonly IDeletableEntityRepository<Grape> grapesRepository;

        public CreateGrapeService(IDeletableEntityRepository<Grape> grapesRepository)
        {
            this.grapesRepository = grapesRepository;
        }

        public async Task CreateAsync(CreateGrapeInputModel input, string userId)
        {
            var grape = this.grapesRepository.All().FirstOrDefault(x => x.Name == input.Name);

            if (grape == null)
            {
                grape = new Grape
                {
                    Name = input.Name,
                };
            }

            if (this.grapesRepository.All().Any(x => x.Name == grape.Name))
            {
                return;
            }

            await this.grapesRepository.AddAsync(grape);
            await this.grapesRepository.SaveChangesAsync();
        }
    }
}

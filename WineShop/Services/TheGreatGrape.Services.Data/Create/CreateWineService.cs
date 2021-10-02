namespace TheGreatGrape.Services.Data.Create
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Web.ViewModels.Wines.Create;

    public class CreateWineService : ICreateWineService
    {
        private readonly IDeletableEntityRepository<Grape> grapesRepository;
        private readonly IDeletableEntityRepository<Winery> wineryRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Wine> wineRepository;
        private readonly ICreateGrapeService createGrapeService;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public CreateWineService(
            IDeletableEntityRepository<Grape> grapesRepository,
            IDeletableEntityRepository<Winery> wineryRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Wine> wineRepository,
            ICreateGrapeService createGrapeService)
        {
            this.grapesRepository = grapesRepository;
            this.wineryRepository = wineryRepository;
            this.categoriesRepository = categoriesRepository;
            this.wineRepository = wineRepository;
            this.createGrapeService = createGrapeService;
        }

        public async Task CreateAsync(CreateWineInputModel input, string userId, string imagePath, bool isApproved)
        {
            var wine = new Wine
            {
                Name = input.Name,
                Year = input.Year,
                Price = input.Price,
                Volume = input.Volume,
                Alcohol = input.Alcohol,
                Description = input.Description,
                CategoryId = input.CategoryId,
                WineryId = input.WineryId,
                CountryId = input.CountryId,
                Sweetness = input.Sweetness,
                IsApproved = isApproved,
            };

            var wineGrape = new WineGrape
            {
                GrapeId = input.GrapeId,
            };

            Directory.CreateDirectory($"{imagePath}/wines/");

            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception("Invalid image format");
            }

            var newImage = new WineImage
            {
                AddedByUserId = userId,
                Extension = extension,
            };

            wine.Image = newImage;

            var path = $"{imagePath}/wines/{newImage.Id}.{newImage.Extension}";

            using Stream fileStream = new FileStream(path, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            wine.Grapes.Add(wineGrape);

            await this.wineRepository.AddAsync(wine);
            await this.wineRepository.SaveChangesAsync();
        }
    }
}

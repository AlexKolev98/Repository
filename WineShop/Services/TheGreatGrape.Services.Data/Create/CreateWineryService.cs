namespace TheGreatGrape.Services.Data.Create
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Web.ViewModels.Wineries.Create;

    public class CreateWineryService : ICreateWineryService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Winery> wineryRepository;

        public CreateWineryService(IDeletableEntityRepository<Winery> wineryRepository)
        {
            this.wineryRepository = wineryRepository;
        }

        public async Task CreateAsync(CreateWineryInputModel input, string userId, string imagePath)
        {
            if (this.wineryRepository.All().FirstOrDefault(x => x.Name == input.Name) != null)
            {
                throw new Exception("Winery already exists");
            }

            var winery = new Winery
            {
                Description = input.Description,
                Name = input.Name,
            };

            Directory.CreateDirectory($"{imagePath}/wineries/");

            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception("Invalid image format");
                }

                var newImage = new WineryImage
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };

                winery.WineryImages.Add(newImage);

                var path = $"{imagePath}/wineries/{newImage.Id}.{newImage.Extension}";

                using Stream fileStream = new FileStream(path, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.wineryRepository.AddAsync(winery);
            await this.wineryRepository.SaveChangesAsync();
        }
    }
}

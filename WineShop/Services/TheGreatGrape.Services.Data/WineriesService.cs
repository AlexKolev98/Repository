namespace TheGreatGrape.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Mapping;
    using TheGreatGrape.Web.ViewModels.Wineries;

    public class WineriesService : IWineriesService
    {
        private readonly IDeletableEntityRepository<Winery> wineryRepository;

        public WineriesService(IDeletableEntityRepository<Winery> wineryRepository)
        {
            this.wineryRepository = wineryRepository;
        }

        public GetWineriesViewModel GetAll()
        {
            var data = new GetWineriesViewModel
            {
                Wineries = this.wineryRepository.AllAsNoTracking().ToList(),
            };
            return data;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var wineries = this.wineryRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>()
                .ToList();
            return wineries;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.wineryRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).OrderByDescending(x => x.Name)
                .ToList()
            .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public int GetCount()
        {
            return this.wineryRepository.AllAsNoTracking().Count();
        }

        public WineryViewModel GetWinery(int id)
        {
            var winery = this.wineryRepository.AllAsNoTracking().Where(x => x.Id == id).To<WineryViewModel>().FirstOrDefault();

            return winery;
        }
    }
}

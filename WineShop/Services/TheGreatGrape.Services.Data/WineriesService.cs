using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Data.Models.WineShop;
using TheGreatGrape.Services.Mapping;
using TheGreatGrape.Web.ViewModels.Wineries;

namespace TheGreatGrape.Services.Data
{
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
            var wines = this.wineryRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>()
                .ToList();
            return wines;
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
            var winery1 = this.wineryRepository.AllAsNoTracking().Where(x => x.Id == id).To<WineryViewModel>();
            var winery = this.wineryRepository.AllAsNoTracking().Where(x => x.Id == id).To<WineryViewModel>().FirstOrDefault();

            return winery;
        }
    }
}

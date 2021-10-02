using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Data.Models.WineShop;

namespace TheGreatGrape.Services.Data
{
    public class CountriesService : ICountriesService
    {
        private readonly IRepository<Country> countriesRepository;

        public CountriesService(IRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.countriesRepository.AllAsNoTracking()
                .OrderBy(x => x.Wines.Count)
                .ThenBy(x => x.Name)
                .Select(x => new
            {
                x.Id,
                x.Name,
            })
            .ToList()
            .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}

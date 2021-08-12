using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Services.Mapping;
using TheGreatGrape.Web.ViewModels.Grapes;

namespace TheGreatGrape.Services.Data
{
    public class GrapesService : IGrapesService
    {
        private readonly IDeletableEntityRepository<Grape> grapesRepository;

        public GrapesService(IDeletableEntityRepository<Grape> grapesRepository)
        {
            this.grapesRepository = grapesRepository;
        }

        public GetGrapesViewModel GetAll()
        {
            var data = new GetGrapesViewModel
            {
                Grapes = this.grapesRepository.AllAsNoTracking().ToList(),
            };
            return data;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var wines = this.grapesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>()
                .ToList();
            return wines;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.grapesRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            })
            .OrderByDescending(x => x.Name)
            .ToList()
            .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public int GetCount()
        {
            return this.grapesRepository.AllAsNoTracking().Count();
        }
    }
}

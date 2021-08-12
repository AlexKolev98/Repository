using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Data.Models.WineShop;
using TheGreatGrape.Services.Mapping;
using TheGreatGrape.Web.ViewModels.Wines;

namespace TheGreatGrape.Services.Data
{
    public class WinesService : IWinesService
    {
        private readonly IDeletableEntityRepository<Wine> winesRepository;

        public WinesService(IDeletableEntityRepository<Wine> winesRepository)
        {
            this.winesRepository = winesRepository;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var wines = this.winesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).To<T>()
                .ToList();
            return wines;
        }

        public int GetCount()
        {
            return this.winesRepository.AllAsNoTracking().Count();
        }

        public T GetWine<T>(int id)
        {
            var wineFromDb = this.winesRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);

            // No Automapper
            //var generateModel = new WineViewModel
            //{
            //    Id = wineFromDb.Id,
            //    Sweetness = wineFromDb.Sweetness,
            //    AddedByUserId = wineFromDb.AddedByUserId,
            //    Alcohol = wineFromDb.Alcohol,
            //    CategoryId = wineFromDb.CategoryId,
            //    CountryId = wineFromDb.CountryId,
            //    Description = wineFromDb.Description,
            //    ImageUrl = wineFromDb.ImageUrl,
            //    Name = wineFromDb.Name,
            //    Price = wineFromDb.Price,
            //    Volume = wineFromDb.Volume,
            //    WineryId = wineFromDb.WineryId,
            //    Year = wineFromDb.Year,
            //    //Subject to change if wine needs more than 1 image.
            //    GrapeId = wineFromDb.Grapes.FirstOrDefault().GrapeId,
            //};

            var wine = this.winesRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return wine;
        }

        public IEnumerable<WinesListViewModel> GetAllByWinery(int page, int itemsPerPage, int wineryId)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.WineryId == wineryId).ToList();

            return wines;
        }

        public IEnumerable<WinesListViewModel> GetAllByGrape(int page, int itemsPerPage, int grapeId)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.GrapeId == grapeId).ToList();

            return wines;
        }

        public IEnumerable<WinesListViewModel> GetAllBySweetness(int page, int itemsPerPage, string sweetness)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.Sweetness.ToString() == sweetness).ToList();

            return wines;
        }

        public IEnumerable<WinesListViewModel> GetAllByCategory(int page, int itemsPerPage, int categoryId)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.CategoryId == categoryId).ToList();

            return wines;
        }

        public IEnumerable<WinesListViewModel> GetAllByCountry(int page, int itemsPerPage, int countryId)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.CountryId == countryId).ToList();

            return wines;
        }

        public IEnumerable<WinesListViewModel> GetAllByYear(int page, int itemsPerPage, int year)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.Year == year).ToList();

            return wines;
        }

        public IEnumerable<WinesListViewModel> GetAllByVolume(int page, int itemsPerPage, int volume)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.Volume == volume).ToList();

            return wines;
        }

        public IEnumerable<WinesListViewModel> GetAllByAlcohol(int page, int itemsPerPage, decimal alcohol)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.Alcohol == alcohol).ToList();

            return wines;
        }
    }
}

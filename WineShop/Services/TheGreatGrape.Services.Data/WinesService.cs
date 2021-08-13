﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Data.Models.TheGreatGrape.Models;
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

        public WineViewModel GetWine(int id)
        {
            // No Automapper
            //var wineFromDb = this.winesRepository.AllAsNoTracking().Where(x => x.Id == id).FirstOrDefault();
          // var generateModel = new WineViewModel
          // {
          //     Id = wineFromDb.Id,
          //     Sweetness = wineFromDb.Sweetness,
          //     AddedByUserId = wineFromDb.AddedByUserId,
          //     Alcohol = wineFromDb.Alcohol,
          //     CategoryId = wineFromDb.CategoryId,
          //     CountryId = wineFromDb.CountryId,
          //     Description = wineFromDb.Description,
          //     ImageUrl = wineFromDb.ImageUrl,
          //     Name = wineFromDb.Name,
          //     Price = wineFromDb.Price,
          //     Volume = wineFromDb.Volume,
          //     WineryId = wineFromDb.WineryId,
          //     Year = wineFromDb.Year,
          //     //Subject to change if wine needs more than 1 image.
          //     Grapes = (IEnumerable<WineGrapeViewModel>)wineFromDb.Grapes,
          //     CategoryName = wineFromDb.Category.Name,
          //     AverageVote = wineFromDb.Votes.Average()
          // };

            var wine = this.winesRepository.AllAsNoTracking().Where(x => x.Id == id).To<WineViewModel>().FirstOrDefault();

            return wine;
        }

        // inputX defines what to search by (category, winery etc.). Then, searchByInput will be converted appropriately.
        public IEnumerable<WinesListViewModel> GetAllByX(int page, int itemsPerPage, string searchByInput, string inputX)
        {
            var trimmedInput = inputX.Trim();

            if (trimmedInput == nameof(Category))
            {
                var categoryId = int.Parse(searchByInput);
                var wines = this.GetAllByCategory(page, itemsPerPage, categoryId);
                return wines;
            }
            else if (trimmedInput == nameof(Country))
            {
                var countryId = int.Parse(searchByInput);
                var wines = this.GetAllByCountry(page, itemsPerPage, countryId);
                return wines;
            }
            else if (trimmedInput == nameof(Winery))
            {
                var wineryId = int.Parse(searchByInput);
                var wines = this.GetAllByWinery(page, itemsPerPage, wineryId);
                return wines;
            }
            else if (trimmedInput == nameof(Grape))
            {
                var grapeId = int.Parse(searchByInput);
                var wines = this.GetAllByGrape(page, itemsPerPage, grapeId);
                return wines;
            }
            else if (trimmedInput == nameof(Wine.Year))
            {
                var year = int.Parse(searchByInput);
                var wines = this.GetAllByYear(page, itemsPerPage, year);
                return wines;
            }
            else if (trimmedInput == nameof(Wine.Volume))
            {
                int volume = int.Parse(searchByInput);
                var wines = this.GetAllByVolume(page, itemsPerPage, volume);
                return wines;
            }
            else if (trimmedInput == nameof(Wine.Alcohol))
            {
                decimal alcohol = decimal.Parse(searchByInput);
                var wines = this.GetAllByAlcohol(page, itemsPerPage, alcohol);
                return wines;
            }
            else if (trimmedInput == nameof(Wine.Sweetness))
            {
                var sweetness = searchByInput;
                var wines = this.GetAllBySweetness(page, itemsPerPage, sweetness);
                return wines;
            }
            else
            {
                return null;
            }
        }

        private IEnumerable<WinesListViewModel> GetAllByWinery(int page, int itemsPerPage, int wineryId)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.WineryId == wineryId).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByGrape(int page, int itemsPerPage, int grapeId)
        {

            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.GrapeId == grapeId).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllBySweetness(int page, int itemsPerPage, string sweetness)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.Sweetness.ToString() == sweetness).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByCategory(int page, int itemsPerPage, int categoryId)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.CategoryId == categoryId).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByCountry(int page, int itemsPerPage, int countryId)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.CountryId == countryId).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByYear(int page, int itemsPerPage, int year)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.Year == year).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByVolume(int page, int itemsPerPage, int volume)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.Volume == volume).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByAlcohol(int page, int itemsPerPage, decimal alcohol)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.Alcohol == alcohol).ToList();

            return wines;
        }
    }
}
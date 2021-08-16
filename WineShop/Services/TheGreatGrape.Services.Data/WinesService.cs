namespace TheGreatGrape.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Mapping;
    using TheGreatGrape.Web.ViewModels.Wines;

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
            var wine = this.winesRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();

            if (wine == null)
            {
                throw new Exception("Looks like you made a pour decision...");
            }

            return wine;
        }

        public async Task TakeAction(int id, string value)
        {
            if (value == "true")
            {
                var wine = this.winesRepository.AllAsNoTracking().Where(x => x.Id == id).FirstOrDefault();
                wine.IsApproved = true;
                this.winesRepository.Update(wine);
                await this.winesRepository.SaveChangesAsync();
            }
            else
            {
                var wine = this.winesRepository.AllAsNoTracking().Where(x => x.Id == id).FirstOrDefault();
                this.winesRepository.Delete(wine);
                await this.winesRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<WinesListViewModel> GetApprovedOnly(int page, int itemsPerPage)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.IsApproved == true).ToList();

            return wines;
        }

        public IEnumerable<WinesListViewModel> GetAllByNotApproved(int page, int itemsPerPage)
        {
            var wines = this.GetAll<WinesListViewModel>(page, itemsPerPage).Where(x => x.IsApproved != true).ToList();

            return wines;
        }

        // inputX defines what to search by (category, winery etc.). Then, searchByInput will be converted appropriately.
        public IEnumerable<WinesListViewModel> GetAllByX(int page, int itemsPerPage, string searchByInput, string inputX, string isComingFrom)
        {
            var trimmedInput = inputX.Trim();

            if (isComingFrom == "WinesController")
            {
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
                else if (trimmedInput == nameof(Wine.IsApproved))
                {
                    var wines = this.GetAllByNotApproved(page, itemsPerPage);
                    return wines;
                }
                else
                {
                    return null;
                }
            }
            else if (isComingFrom == "Search")
            {

            }

            return null;
        }

        private IEnumerable<WinesListViewModel> GetAllByWinery(int page, int itemsPerPage, int wineryId)
        {
            var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.WineryId == wineryId).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByGrape(int page, int itemsPerPage, int grapeId)
        {
            var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.GrapeId == grapeId).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllBySweetness(int page, int itemsPerPage, string sweetness)
        {
            var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.Sweetness.ToString() == sweetness).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByCategory(int page, int itemsPerPage, int categoryId)
        {
            var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.CategoryId == categoryId).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByCountry(int page, int itemsPerPage, int countryId)
        {
            var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.CountryId == countryId).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByYear(int page, int itemsPerPage, int year)
        {
            var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.Year == year).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByVolume(int page, int itemsPerPage, int volume)
        {
            var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.Volume == volume).ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByAlcohol(int page, int itemsPerPage, decimal alcohol)
        {
            var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.Alcohol == alcohol).ToList();

            return wines;
        }
    }
}

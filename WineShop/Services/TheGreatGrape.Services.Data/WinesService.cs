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

        // This service is used by 2 (Wine / Search) controllers.
        // It generates the appropriate viewModel by hard or soft search.

        // inputX defines what to search by (category, winery etc.).
        // The private services do the rest by converting searchByInput to the appropriate type, depending on which controller this was called by.
        public IEnumerable<WinesListViewModel> GetAllByX(int page, int itemsPerPage, string searchByInput, string inputX, string isComingFrom)
        {
            inputX.Trim();

            if (inputX == nameof(Category))
            {
                return this.GetAllByCategory(page, itemsPerPage, searchByInput, isComingFrom);
            }
            else if (inputX == nameof(Country))
            {
                return this.GetAllByCountry(page, itemsPerPage, searchByInput, isComingFrom);
            }
            else if (inputX == nameof(Wine.Name))
            {
                return this.GetAllByName(page, itemsPerPage, searchByInput, isComingFrom);
            }
            else if (inputX == nameof(Winery))
            {
                return this.GetAllByWinery(page, itemsPerPage, searchByInput, isComingFrom);
            }
            else if (inputX == nameof(Grape))
            {
                return this.GetAllByGrape(page, itemsPerPage, searchByInput, isComingFrom);
            }
            else if (inputX == nameof(Wine.Price))
            {
                return this.GetAllByPrice(page, itemsPerPage, searchByInput);
            }
            else if (inputX == nameof(Wine.Year))
            {
                return this.GetAllByYear(page, itemsPerPage, searchByInput);
            }
            else if (inputX == nameof(Wine.Volume))
            {
                return this.GetAllByVolume(page, itemsPerPage, searchByInput);
            }
            else if (inputX == nameof(Wine.Alcohol))
            {
                return this.GetAllByAlcohol(page, itemsPerPage, searchByInput);
            }
            else if (inputX == nameof(Wine.Sweetness))
            {
                var sweetness = searchByInput;
                var wines = this.GetAllBySweetness(page, itemsPerPage, sweetness);
                return wines;
            }
            else if (inputX == nameof(Wine.IsApproved))
            {
                var wines = this.GetAllByNotApproved(page, itemsPerPage);
                return wines;
            }
            else
            {
                return null;
            }
        }

        private IEnumerable<WinesListViewModel> GetAllByCategory(int page, int itemsPerPage, string searchByInput, string isComingFrom)
        {
            if (isComingFrom == "WinesController")
            {
                var categoryId = int.Parse(searchByInput);
                var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.CategoryId == categoryId).ToList();
                return wines;
            }
            else
            {
                if (this.winesRepository.All().Any(x => x.Category.Name == searchByInput))
                {
                    var wines = this.winesRepository.All().Where(x => x.Category.Name == searchByInput).To<WinesListViewModel>().ToList();
                    return wines;
                }

                return null;
            }
        }

        private IEnumerable<WinesListViewModel> GetAllByCountry(int page, int itemsPerPage, string searchByInput, string isComingFrom)
        {
            if (isComingFrom == "WinesController")
            {
                var countryId = int.Parse(searchByInput);
                var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.CountryId == countryId).ToList();
                return wines;
            }
            else
            {
                if (this.winesRepository.All().Any(x => x.Country.Name == searchByInput))
                {
                    var wines = this.winesRepository.All().Where(x => x.Country.Name == searchByInput && x.IsApproved == true).To<WinesListViewModel>().ToList();
                    return wines;
                }

                return null;
            }
        }

        private IEnumerable<WinesListViewModel> GetAllByWinery(int page, int itemsPerPage, string searchByInput, string isComingFrom)
        {
            if (isComingFrom == "WinesController")
            {
                var wineryId = int.Parse(searchByInput);
                var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.WineryId == wineryId).ToList();
                return wines;
            }
            else
            {
                if (this.winesRepository.All().Any(x => x.Winery.Name == searchByInput))
                {
                    var wines = this.winesRepository.All().Where(x => x.Winery.Name == searchByInput && x.IsApproved == true).To<WinesListViewModel>().ToList();
                    return wines;
                }

                return null;
            }
        }

        private IEnumerable<WinesListViewModel> GetAllByGrape(int page, int itemsPerPage, string searchByInput, string isComingFrom)
        {
            if (isComingFrom == "WinesController")
            {
                var grapeId = int.Parse(searchByInput);
                var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.GrapeId == grapeId).ToList();
                return wines;
            }
            else
            {
                //Subject to change if wine uses more than 1 grape.
                if (this.winesRepository.All().Any(x => x.Grapes.FirstOrDefault().Grape.Name == searchByInput))
                {
                    var wines = this.winesRepository.All().Where(x => x.Grapes.FirstOrDefault().Grape.Name == searchByInput && x.IsApproved == true).To<WinesListViewModel>().ToList();
                    return wines;
                }

                return null;
            }
        }

        private IEnumerable<WinesListViewModel> GetAllByPrice(int page, int itemsPerPage, string searchByInput)
        {
            if (decimal.TryParse(searchByInput, out decimal price))
            {
                if (this.winesRepository.All().Any(x => x.Price == price && x.IsApproved == true))
                {
                    var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.Price == price).ToList();
                    return wines;
                }
            }

            return null;
        }

        private IEnumerable<WinesListViewModel> GetAllByYear(int page, int itemsPerPage, string searchByInput)
        {
            if (int.TryParse(searchByInput, out int year))
            {
                if (this.winesRepository.All().Any(x => x.Year == year && x.IsApproved == true))
                {
                    var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.Year == year).ToList();
                    return wines;
                }
            }

            return null;
        }

        private IEnumerable<WinesListViewModel> GetAllByVolume(int page, int itemsPerPage, string searchByInput)
        {
            if (int.TryParse(searchByInput, out int volume))
            {
                if (this.winesRepository.All().Any(x => x.Volume == volume))
                {
                    var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.Volume == volume).ToList();
                    return wines;
                }
            }

            return null;
        }

        private IEnumerable<WinesListViewModel> GetAllByAlcohol(int page, int itemsPerPage, string searchByInput)
        {
            if (decimal.TryParse(searchByInput, out decimal alcohol))
            {
                if (this.winesRepository.All().Any(x => x.Alcohol == alcohol && x.IsApproved == true))
                {
                    var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.Alcohol == alcohol).ToList();
                    return wines;
                }
            }

            return null;
        }

        private IEnumerable<WinesListViewModel> GetAllByName(int page, int itemsPerPage, string searchByInput, string isComingFrom)
        {
            if (isComingFrom == "WinesController")
            {
                var name = searchByInput;
                var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.Name == name).ToList();
                return wines;
            }
            else
            {
                if (this.winesRepository.All().Any(x => x.Name == searchByInput) || this.winesRepository.All().Any(x => x.Name.ToLower() == searchByInput))
                {
                    var wines = this.winesRepository.All().Where(x => (x.Name == searchByInput || x.Name.ToLower() == searchByInput) && x.IsApproved == true).To<WinesListViewModel>().ToList();
                    return wines;
                }
                else
                {
                    // Split each wine name by whitespace (" "), then find any matching components.
                    var wines = new List<WinesListViewModel>();
                    var winesNameAndId = this.winesRepository.All().Select(x => new { x.Name, x.Id });
                    foreach (var wine in winesNameAndId)
                    {
                        var nameComponents = wine.Name.Split(" ");
                        if (nameComponents.Any(x => x == searchByInput) || nameComponents.Any(x => x.ToLower() == searchByInput))
                        {
                            var wineToAdd = this.winesRepository.All().Where(x => x.Id == wine.Id && x.IsApproved == true).To<WinesListViewModel>().FirstOrDefault();
                            wines.Add(wineToAdd);
                        }
                    }

                    if (wines.Count == 0)
                    {
                        return null;
                    }

                    return wines;
                }
            }
        }
        private IEnumerable<WinesListViewModel> GetAllBySweetness(int page, int itemsPerPage, string sweetness)
        {
            var wines = this.GetApprovedOnly(page, itemsPerPage).Where(x => x.Sweetness.ToString() == sweetness).ToList();

            return wines;
        }
    }
}

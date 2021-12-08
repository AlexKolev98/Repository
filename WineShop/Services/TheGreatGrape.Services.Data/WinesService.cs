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

        public IEnumerable<WinesListViewModel> GetModelWithPaging(int page, int itemsPerPage, IEnumerable<WinesListViewModel> viewModel)
        {
            return this.ApplyPaging(page, itemsPerPage, viewModel);
        }

        public IEnumerable<WinesListViewModel> GetAllByX(string searchByInput, string searchBy, string isComingFrom)
        {
            // This method is used by 2 (Wine / Search) controllers.
            // It generates the appropriate viewModel by hard or soft search.

            // searchBy defines what to search by (category, winery etc.).
            // The private methods do the rest by converting searchByInput to the appropriate type and apply paging, depending on which controller this was called by.
            searchBy.Trim();

            if (searchBy == nameof(Category))
            {
                return this.GetAllByCategory(searchByInput, isComingFrom);
            }
            else if (searchBy == nameof(Country))
            {
                return this.GetAllByCountry(searchByInput, isComingFrom);
            }
            else if (searchBy == nameof(Wine.Name))
            {
                return this.GetAllByName(searchByInput, isComingFrom);
            }
            else if (searchBy == nameof(Winery))
            {
                return this.GetAllByWinery(searchByInput, isComingFrom);
            }
            else if (searchBy == nameof(Grape))
            {
                return this.GetAllByGrape(searchByInput, isComingFrom);
            }
            else if (searchBy == nameof(Wine.Price))
            {
                return this.GetAllByPrice(searchByInput);
            }
            else if (searchBy == nameof(Wine.Year))
            {
                return this.GetAllByYear(searchByInput);
            }
            else if (searchBy == nameof(Wine.Volume))
            {
                return this.GetAllByVolume(searchByInput);
            }
            else if (searchBy == nameof(Wine.Alcohol))
            {
                return this.GetAllByAlcohol(searchByInput);
            }
            else if (searchBy == nameof(Wine.Sweetness))
            {
                return this.GetAllBySweetness(searchByInput);
            }
            else if (searchBy == nameof(Wine.IsApproved))
            {
                return this.GetAllByNotApproved();
            }
            else
            {
                return null;
            }
        }

        public int GetCount()
        {
            // Returns the count of all approved wines.
            return this.winesRepository.AllAsNoTracking().Where(x => x.IsApproved == true).Count();
        }

        public int GetCount(string searchByInput, string searchBy, string isComingFrom)
        {
            // Gets count of wines by search criteria.
            // Critical for correct paging.
            return this.GetAllByX(searchByInput, searchBy, isComingFrom).Count();
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

        public T GetWineDespiteDeleted<T>(int id)
        {
            var wine = this.winesRepository.AllAsNoTrackingWithDeleted().Where(x => x.Id == id).To<T>().FirstOrDefault();

            if (wine == null)
            {
                throw new Exception("Looks like you made a pour decision...");
            }

            return wine;
        }

        public async Task ApproveOrRemove(int id, string value)
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

        public IEnumerable<WinesListViewModel> GetApprovedOnly()
        {
            // Returns all approved wines and applies paging.
            var wines = this.GetAllWithoutPaging<WinesListViewModel>().Where(x => x.IsApproved == true);
            return wines;
        }

        public IEnumerable<WinesListViewModel> GetAllByNotApproved()
        {
            // Returns all wines that are not approved and applies paging.
            var wines = this.GetAllWithoutPaging<WinesListViewModel>().Where(x => x.IsApproved != true);

            return wines;
        }

        private IEnumerable<WinesListViewModel> ApplyPaging(int page, int itemsPerPage, IEnumerable<WinesListViewModel> list)
        {
            // Applies paging to the view model.
            return list.OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
        }

        private IEnumerable<T> GetAllWithoutPaging<T>()
        {
            // Returns all approved wines.
            // Paging should be applied later.
            return this.winesRepository.AllAsNoTracking().To<T>().ToList();
        }

        private IEnumerable<WinesListViewModel> GetApprovedOnlyPrivate()
        {
            // This method is used only by the methods below it.
            // Returns all approved wines.
            var wines = this.GetAllWithoutPaging<WinesListViewModel>().Where(x => x.IsApproved == true).OrderByDescending(x => x.Id)
                .ToList();

            return wines;
        }

        private IEnumerable<WinesListViewModel> GetAllByCategory(string searchByInput, string isComingFrom)
        {
            if (isComingFrom == "WinesController")
            {
                var categoryId = int.Parse(searchByInput);
                var wines = this.GetApprovedOnlyPrivate().Where(x => x.CategoryId == categoryId);
                return wines;
            }
            else
            {
                if (this.winesRepository.All().Any(x => x.Category.Name == searchByInput))
                {
                    var wines = this.winesRepository.All().Where(x => x.Category.Name == searchByInput).To<WinesListViewModel>();
                    return wines;
                }

                return null;
            }
        }

        private IEnumerable<WinesListViewModel> GetAllByCountry(string searchByInput, string isComingFrom)
        {
            if (isComingFrom == "WinesController")
            {
                var countryId = int.Parse(searchByInput);
                var wines = this.GetApprovedOnlyPrivate().Where(x => x.CountryId == countryId);
                return wines;
            }
            else
            {
                if (this.winesRepository.All().Any(x => x.Country.Name == searchByInput))
                {
                    var wines = this.winesRepository.All().Where(x => x.Country.Name == searchByInput && x.IsApproved == true).To<WinesListViewModel>();
                    return wines;
                }

                return null;
            }
        }

        private IEnumerable<WinesListViewModel> GetAllByWinery(string searchByInput, string isComingFrom)
        {
            if (isComingFrom == "WinesController")
            {
                var wineryId = int.Parse(searchByInput);
                var wines = this.GetApprovedOnlyPrivate().Where(x => x.WineryId == wineryId);
                return wines;
            }
            else
            {
                if (this.winesRepository.All().Any(x => x.Winery.Name == searchByInput))
                {
                    var wines = this.winesRepository.All().Where(x => x.Winery.Name == searchByInput && x.IsApproved == true).To<WinesListViewModel>();
                    return wines;
                }

                return null;
            }
        }

        private IEnumerable<WinesListViewModel> GetAllByGrape(string searchByInput, string isComingFrom)
        {
            if (isComingFrom == "WinesController")
            {
                var grapeId = int.Parse(searchByInput);
                var wines = this.GetApprovedOnlyPrivate().Where(x => x.GrapeId == grapeId).ToList();
                return wines;
            }
            else
            {
                // Subject to change if wine uses more than 1 grape.
                if (this.winesRepository.All().Any(x => x.Grapes.FirstOrDefault().Grape.Name == searchByInput))
                {
                    var wines = this.winesRepository.All().Where(x => x.Grapes.FirstOrDefault().Grape.Name == searchByInput && x.IsApproved == true).To<WinesListViewModel>();
                    return wines;
                }

                return null;
            }
        }

        private IEnumerable<WinesListViewModel> GetAllByPrice(string searchByInput)
        {
            if (decimal.TryParse(searchByInput, out decimal price))
            {
                if (this.winesRepository.All().Any(x => x.Price == price && x.IsApproved == true))
                {
                    var wines = this.GetApprovedOnlyPrivate().Where(x => x.Price == price);
                    return wines;
                }
            }

            return null;
        }

        private IEnumerable<WinesListViewModel> GetAllByYear(string searchByInput)
        {
            if (int.TryParse(searchByInput, out int year))
            {
                if (this.winesRepository.All().Any(x => x.Year == year && x.IsApproved == true))
                {
                    var wines = this.GetApprovedOnlyPrivate().Where(x => x.Year == year);
                    return wines;
                }
            }

            return null;
        }

        private IEnumerable<WinesListViewModel> GetAllByVolume(string searchByInput)
        {
            if (int.TryParse(searchByInput, out int volume))
            {
                if (this.winesRepository.All().Any(x => x.Volume == volume))
                {
                    var wines = this.GetApprovedOnlyPrivate().Where(x => x.Volume == volume);
                    return wines;
                }
            }

            return null;
        }

        private IEnumerable<WinesListViewModel> GetAllByAlcohol(string searchByInput)
        {
            if (decimal.TryParse(searchByInput, out decimal alcohol))
            {
                if (this.winesRepository.All().Any(x => x.Alcohol == alcohol && x.IsApproved == true))
                {
                    var wines = this.GetApprovedOnlyPrivate().Where(x => x.Alcohol == alcohol);
                    return wines;
                }
            }

            return null;
        }

        private IEnumerable<WinesListViewModel> GetAllByName(string searchByInput, string isComingFrom)
        {
            if (isComingFrom == "WinesController")
            {
                var name = searchByInput;
                var wines = this.GetApprovedOnlyPrivate().Where(x => x.Name == name);
                return wines;
            }
            else
            {
                if (this.winesRepository.All().Any(x => x.Name == searchByInput) || this.winesRepository.All().Any(x => x.Name.ToLower() == searchByInput))
                {
                    var wines = this.winesRepository.All().Where(x => (x.Name == searchByInput || x.Name.ToLower() == searchByInput) && x.IsApproved == true).To<WinesListViewModel>();
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
                        var searchComponents = searchByInput.Split(" ");
                        foreach (var component in nameComponents)
                        {
                            if (searchComponents.Any(x => x == component))
                            {
                                var wineToAdd = this.winesRepository.All().Where(x => x.Id == wine.Id && x.IsApproved == true).To<WinesListViewModel>().FirstOrDefault();

                                if (wines.Contains(wineToAdd))
                                {
                                    continue;
                                }

                                wines.Add(wineToAdd);
                            }
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

        private IEnumerable<WinesListViewModel> GetAllBySweetness(string sweetness)
        {
            var wines = this.GetApprovedOnlyPrivate().Where(x => x.Sweetness.ToString() == sweetness);
            return wines;
        }
    }
}

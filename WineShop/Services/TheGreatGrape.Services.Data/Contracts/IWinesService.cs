namespace TheGreatGrape.Services.Data
{
    using System.Collections.Generic;

    using TheGreatGrape.Web.ViewModels.Wines;

    public interface IWinesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        int GetCount();

        public T GetWine<T>(int id);

        public IEnumerable<WinesListViewModel> GetAllByWinery(int page, int itemsPerPage, int wineryId);

        public IEnumerable<WinesListViewModel> GetAllByGrape(int page, int itemsPerPage, int grapeId);

        public IEnumerable<WinesListViewModel> GetAllBySweetness(int page, int itemsPerPage, string sweetness);

        public IEnumerable<WinesListViewModel> GetAllByCategory(int page, int itemsPerPage, int categoryId);

        public IEnumerable<WinesListViewModel> GetAllByCountry(int page, int itemsPerPage, int countryId);

        public IEnumerable<WinesListViewModel> GetAllByYear(int page, int itemsPerPage, int year);

        public IEnumerable<WinesListViewModel> GetAllByVolume(int page, int itemsPerPage, int volume);

        public IEnumerable<WinesListViewModel> GetAllByAlcohol(int page, int itemsPerPage, decimal alcohol);
    }
}

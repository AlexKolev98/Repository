namespace TheGreatGrape.Services.Data
{
    using System.Collections.Generic;

    using TheGreatGrape.Web.ViewModels.Wines;

    public interface IWinesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        int GetCount();

        public WineViewModel GetWine(int id);

        public IEnumerable<WinesListViewModel> GetAllByX(int page, int itemsPerPage, string searchByInput, string inputX);
    }
}

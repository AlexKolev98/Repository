namespace TheGreatGrape.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TheGreatGrape.Web.ViewModels.Wines;

    public interface IWinesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        int GetCount();

        public T GetWine<T>(int id);

        public IEnumerable<WinesListViewModel> GetAllByX(int page, int itemsPerPage, string searchByInput, string inputX, string isComingFrom);

        public IEnumerable<WinesListViewModel> GetApprovedOnly(int page, int itemsPerPage);

        public IEnumerable<WinesListViewModel> GetAllByNotApproved(int page, int itemsPerPage);

        Task ApproveOrRemove(int id, string value);
    }
}

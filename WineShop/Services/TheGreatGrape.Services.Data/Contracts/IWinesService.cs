namespace TheGreatGrape.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TheGreatGrape.Web.ViewModels.Wines;

    public interface IWinesService
    {
        int GetCount();

        int GetCount(string searchByInput, string searchBy, string isComingFrom);

        public T GetWine<T>(int id);

        public T GetWineDespiteDeleted<T>(int id);

        public IEnumerable<WinesListViewModel> GetAllByX(string searchByInput, string inputX, string isComingFrom);

        public IEnumerable<WinesListViewModel> GetApprovedOnly();

        public IEnumerable<WinesListViewModel> GetAllByNotApproved();

        public IEnumerable<WinesListViewModel> GetModelWithPaging(int page, int itemsPerPage, IEnumerable<WinesListViewModel> viewModel);

        Task ApproveOrRemove(int id, string value);
    }
}

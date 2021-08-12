namespace TheGreatGrape.Services.Data
{
    using System.Collections.Generic;

    using TheGreatGrape.Web.ViewModels.Grapes;

    public interface IGrapesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        public GetGrapesViewModel GetAll();

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        int GetCount();
    }
}

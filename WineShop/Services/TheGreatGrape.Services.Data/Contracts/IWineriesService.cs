﻿namespace TheGreatGrape.Services.Data
{
    using System.Collections.Generic;

    using TheGreatGrape.Web.ViewModels.Wineries;

    public interface IWineriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        public GetWineriesViewModel GetAll();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        int GetCount();

        public T GetWinery<T>(int id);
    }
}

namespace TheGreatGrape.Services.Data.Create
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using TheGreatGrape.Web.ViewModels.Grapes.Create;

    public interface ICreateGrapeService
    {
        public Task CreateAsync(CreateGrapeInputModel input, string userId);
    }
}

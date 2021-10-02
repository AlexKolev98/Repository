namespace TheGreatGrape.Services.Data.Create
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using TheGreatGrape.Web.ViewModels.Wines.Create;

    public interface ICreateWineService
    {
        public Task CreateAsync(CreateWineInputModel input, string userId, string imagePath, bool isApproved);
    }
}

namespace TheGreatGrape.Services.Data.Create
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using TheGreatGrape.Web.ViewModels.Wineries.Create;

    public interface ICreateWineryService
    {
        public Task CreateAsync(CreateWineryInputModel input, string userId, string imagePath);
    }
}

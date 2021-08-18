namespace TheGreatGrape.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using TheGreatGrape.Data;
    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Data.Models.WineShop;
    using TheGreatGrape.Services.Data;
    using TheGreatGrape.Web.ViewModels.Wines;

    [Area("Administration")]
    public class WinesController : AdministrationController
    {
        private const int ItemsPerPage = 12;

        private readonly IDeletableEntityRepository<Wine> winesRepository;
        private readonly ApplicationDbContext db;
        private readonly IWinesService winesService;

        public WinesController(
            IDeletableEntityRepository<Wine> context,
            ApplicationDbContext db,
            IWinesService winesService)
        {
            this.winesRepository = context;
            this.db = db;
            this.winesService = winesService;
        }

        [Authorize]
        public IActionResult AllByNotApproved(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new WinesListViewModel
            {
                PageNumber = id,
                ItemsCount = this.winesService.GetCount(),
                ItemsPerPage = ItemsPerPage,
                Wines = this.winesService.GetAllByNotApproved(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AllByNotApproved(string value, int id)
        {
            await this.winesService.ApproveOrRemove(id, value);
            return this.Redirect("/Administration/Wines/AllByNotApproved");
        }

        public IActionResult ById(int id)
        {
            try
            {
                var viewModel = this.winesService.GetWineDespiteDeleted<WineViewModel>(id);

                if ((viewModel.IsApproved == false && !this.User.IsInRole("Administrator")) || viewModel == null)
                {
                    return this.NotFound();
                }

                return this.View(viewModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.Redirect("/Wines");
            }
        }

        // GET: Administration/Wines
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.winesRepository.AllAsNoTrackingWithDeleted().Include(w => w.AddedByUser).Include(w => w.Category).Include(w => w.Country).Include(w => w.Winery);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Wines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var wine = await this.winesRepository.All()
                .Include(w => w.AddedByUser)
                .Include(w => w.Category)
                .Include(w => w.Country)
                .Include(w => w.Winery)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wine == null)
            {
                return this.NotFound();
            }

            return this.View(wine);
        }

        // GET: Administration/Wines/Create
        public IActionResult Create()
        {
            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id");
            this.ViewData["CategoryId"] = new SelectList(this.db.Categories, "Id", "Name");
            this.ViewData["CountryId"] = new SelectList(this.db.Countries, "Id", "Id");
            this.ViewData["WineryId"] = new SelectList(this.db.Wineries, "Id", "Description");
            return this.View();
        }

        // POST: Administration/Wines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Alcohol,Volume,Year,WineryId,Description,AddedByUserId,CategoryId,ImageUrl,CountryId,Sweetness,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Wine wine)
        {
            if (this.ModelState.IsValid)
            {
                await this.winesRepository.AddAsync(wine);
                await this.winesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", wine.AddedByUserId);
            this.ViewData["CategoryId"] = new SelectList(this.db.Categories, "Id", "Name", wine.CategoryId);
            this.ViewData["CountryId"] = new SelectList(this.db.Countries, "Id", "Id", wine.CountryId);
            this.ViewData["WineryId"] = new SelectList(this.db.Wineries, "Id", "Description", wine.WineryId);
            return this.View(wine);
        }

        // GET: Administration/Wines/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var wine = this.winesRepository.All().FirstOrDefault(x => x.Id == id);
            if (wine == null)
            {
                return this.NotFound();
            }

            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", wine.AddedByUserId);
            this.ViewData["CategoryId"] = new SelectList(this.db.Categories, "Id", "Name", wine.CategoryId);
            this.ViewData["CountryId"] = new SelectList(this.db.Countries, "Id", "Id", wine.CountryId);
            this.ViewData["WineryId"] = new SelectList(this.db.Wineries, "Id", "Description", wine.WineryId);
            return this.View(wine);
        }

        // POST: Administration/Wines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Price,Alcohol,Volume,Year,WineryId,Description,AddedByUserId,CategoryId,ImageUrl,CountryId,Sweetness,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Wine wine)
        {
            if (id != wine.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.winesRepository.Update(wine);
                    await this.winesRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.WineExists(wine.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", wine.AddedByUserId);
            this.ViewData["CategoryId"] = new SelectList(this.db.Categories, "Id", "Name", wine.CategoryId);
            this.ViewData["CountryId"] = new SelectList(this.db.Countries, "Id", "Id", wine.CountryId);
            this.ViewData["WineryId"] = new SelectList(this.db.Wineries, "Id", "Description", wine.WineryId);
            return this.View(wine);
        }

        // GET: Administration/Wines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var wine = await this.winesRepository.All()
                .Include(w => w.AddedByUser)
                .Include(w => w.Category)
                .Include(w => w.Country)
                .Include(w => w.Winery)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wine == null)
            {
                return this.NotFound();
            }

            return this.View(wine);
        }

        // POST: Administration/Wines/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wine = this.winesRepository.All().FirstOrDefault(x => x.Id == id);
            this.winesRepository.Delete(wine);
            await this.winesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool WineExists(int id)
        {
            return this.winesRepository.All().Any(e => e.Id == id);
        }
    }
}

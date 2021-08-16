namespace TheGreatGrape.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using TheGreatGrape.Data;
    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Data.Models.WineShop;

    [Area("Administration")]
    public class WineriesController : Controller
    {
        private readonly IDeletableEntityRepository<Winery> wineryRepository;
        private readonly ApplicationDbContext db;

        public WineriesController(IDeletableEntityRepository<Winery> wineryRepository, ApplicationDbContext db)
        {
            this.wineryRepository = wineryRepository;
            this.db = db;
        }

        // GET: Administration/Wineries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.wineryRepository.All().Include(w => w.AddedByUser);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Wineries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var winery = await this.wineryRepository.All()
                .Include(w => w.AddedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (winery == null)
            {
                return this.NotFound();
            }

            return this.View(winery);
        }

        // GET: Administration/Wineries/Create
        public IActionResult Create()
        {
            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Wineries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,AddedByUserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Winery winery)
        {
            if (this.ModelState.IsValid)
            {
                await this.wineryRepository.AddAsync(winery);
                await this.wineryRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", winery.AddedByUserId);
            return this.View(winery);
        }

        // GET: Administration/Wineries/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var winery = this.wineryRepository.All().FirstOrDefault(x => x.Id == id);
            if (winery == null)
            {
                return this.NotFound();
            }

            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", winery.AddedByUserId);
            return this.View(winery);
        }

        // POST: Administration/Wineries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,AddedByUserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Winery winery)
        {
            if (id != winery.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.wineryRepository.Update(winery);
                    await this.wineryRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.WineryExists(winery.Id))
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

            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", winery.AddedByUserId);
            return this.View(winery);
        }

        // GET: Administration/Wineries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var winery = await this.wineryRepository.All()
                .Include(w => w.AddedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (winery == null)
            {
                return this.NotFound();
            }

            return this.View(winery);
        }

        // POST: Administration/Wineries/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var winery = this.wineryRepository.All().FirstOrDefault(x => x.Id == id);
            this.wineryRepository.Delete(winery);
            await this.wineryRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool WineryExists(int id)
        {
            return this.wineryRepository.All().Any(e => e.Id == id);
        }
    }
}

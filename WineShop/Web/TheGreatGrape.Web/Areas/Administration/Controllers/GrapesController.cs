namespace TheGreatGrape.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using TheGreatGrape.Data;
    using TheGreatGrape.Data.Common.Repositories;

    [Area("Administration")]
    public class GrapesController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<Grape> grapesRepository;

        public GrapesController(ApplicationDbContext db, IDeletableEntityRepository<Grape> grapesRepository)
        {
            this.db = db;
            this.grapesRepository = grapesRepository;
        }

        // GET: Administration/Grapes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.grapesRepository.AllAsNoTrackingWithDeleted().Include(g => g.AddedByUser);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Grapes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var grape = await this.grapesRepository.All()
                .Include(g => g.AddedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grape == null)
            {
                return this.NotFound();
            }

            return this.View(grape);
        }

        // GET: Administration/Grapes/Create
        public IActionResult Create()
        {
            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Grapes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,AddedByUserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Grape grape)
        {
            if (this.ModelState.IsValid)
            {
                await this.grapesRepository.AddAsync(grape);
                await this.grapesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", grape.AddedByUserId);
            return this.View(grape);
        }

        // GET: Administration/Grapes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var grape = this.grapesRepository.All().FirstOrDefault(x => x.Id == id);
            if (grape == null)
            {
                return this.NotFound();
            }

            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", grape.AddedByUserId);
            return this.View(grape);
        }

        // POST: Administration/Grapes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,AddedByUserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Grape grape)
        {
            if (id != grape.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.grapesRepository.Update(grape);
                    await this.grapesRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.GrapeExists(grape.Id))
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

            this.ViewData["AddedByUserId"] = new SelectList(this.db.Users, "Id", "Id", grape.AddedByUserId);
            return this.View(grape);
        }

        // GET: Administration/Grapes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var grape = await this.db.Grapes
                .Include(g => g.AddedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grape == null)
            {
                return this.NotFound();
            }

            return this.View(grape);
        }

        // POST: Administration/Grapes/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grape = this.grapesRepository.All().FirstOrDefault(x => x.Id == id);
            this.grapesRepository.Delete(grape);
            await this.grapesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool GrapeExists(int id)
        {
            return this.grapesRepository.All().Any(e => e.Id == id);
        }
    }
}

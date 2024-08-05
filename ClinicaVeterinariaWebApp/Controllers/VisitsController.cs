using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaVeterinariaWebApp.Data;
using ClinicaVeterinariaWebApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWebApp.Controllers
{
    public class VisitsController : Controller
    {
        private readonly VeterinaryContext _context;

        public VisitsController(VeterinaryContext context)
        {
            _context = context;
        }

        // GET: Visits
        public async Task<IActionResult> Index()
        {
            var veterinaryContext = _context.Visits.Include(v => v.Animal);
            return View(await veterinaryContext.ToListAsync());
        }

        // GET: Visits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            ViewBag.Animals = new SelectList(_context.Animals.Select(a => new
            {
                Id = a.Id,
                Description = a.Name + " - " + a.OwnerFirstName + " " + a.OwnerLastName
            }), "Id", "Description");
            return View();
        }

        // POST: Visits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VisitDate,ObjectiveExam,PrescribedTreatment,AnimalId")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Animals = new SelectList(_context.Animals.Select(a => new
            {
                Id = a.Id,
                Description = a.Name + " - " + a.OwnerFirstName + " " + a.OwnerLastName
            }), "Id", "Description", visit.AnimalId);
            return View(visit);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            ViewBag.Animals = new SelectList(_context.Animals.Select(a => new
            {
                Id = a.Id,
                Description = a.Name + " - " + a.OwnerFirstName + " " + a.OwnerLastName
            }), "Id", "Description", visit.AnimalId);
            return View(visit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VisitDate,ObjectiveExam,PrescribedTreatment,AnimalId")] Visit visit)
        {
            if (id != visit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Animals = new SelectList(_context.Animals.Select(a => new
            {
                Id = a.Id,
                Description = a.Name + " - " + a.OwnerFirstName + " " + a.OwnerLastName
            }), "Id", "Description", visit.AnimalId);
            return View(visit);
        }


        // GET: Visits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visit = await _context.Visits.FindAsync(id);
            if (visit != null)
            {
                _context.Visits.Remove(visit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicaVeterinariaWebApp.Data;
using ClinicaVeterinariaWebApp.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClinicaVeterinariaWebApp.Controllers
{
    public class SheltersController : Controller
    {
        private readonly VeterinaryContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SheltersController(VeterinaryContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Shelters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shelters.ToListAsync());
        }

        // GET: Shelters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // GET: Shelters/Create
        public IActionResult Create()
        {
            ViewBag.AnimalTypes = new SelectList(new[] { "Cane", "Gatto" });
            return View();
        }

        // POST: Shelters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegistrationDate,Name,Type,CoatColor,BirthDate,Microchip,AdmissionDate,Photo")] Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                if (shelter.Photo != null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + shelter.Photo.FileName;
                    string filePath = Path.Combine(_hostEnvironment.WebRootPath, "img", uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await shelter.Photo.CopyToAsync(fileStream);
                    }
                    shelter.PhotoUrl = "/img/" + uniqueFileName;
                }

                _context.Add(shelter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AnimalTypes = new SelectList(new[] { "Cane", "Gatto" });
            return View(shelter);
        }

        // GET: Shelters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelters.FindAsync(id);
            if (shelter == null)
            {
                return NotFound();
            }
            ViewBag.AnimalTypes = new SelectList(new[] { "Cane", "Gatto" });
            return View(shelter);
        }

        // POST: Shelters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegistrationDate,Name,Type,CoatColor,BirthDate,Microchip,AdmissionDate,DischargeDate,PhotoUrl,Photo")] Shelter shelter)
        {
            if (id != shelter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (shelter.Photo != null)
                    {
                        // Elimina la vecchia foto se presente
                        if (!string.IsNullOrEmpty(shelter.PhotoUrl))
                        {
                            string oldFilePath = Path.Combine(_hostEnvironment.WebRootPath, shelter.PhotoUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        // Carica la nuova foto
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + shelter.Photo.FileName;
                        string filePath = Path.Combine(_hostEnvironment.WebRootPath, "img", uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await shelter.Photo.CopyToAsync(fileStream);
                        }
                        shelter.PhotoUrl = "/img/" + uniqueFileName;
                    }

                    _context.Update(shelter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShelterExists(shelter.Id))
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
            ViewBag.AnimalTypes = new SelectList(new[] { "Cane", "Gatto" });
            return View(shelter);
        }

        // GET: Shelters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // POST: Shelters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shelter = await _context.Shelters.FindAsync(id);
            if (shelter != null)
            {
                if (!string.IsNullOrEmpty(shelter.PhotoUrl))
                {
                    string filePath = Path.Combine(_hostEnvironment.WebRootPath, shelter.PhotoUrl.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                _context.Shelters.Remove(shelter);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShelterExists(int id)
        {
            return _context.Shelters.Any(e => e.Id == id);
        }

        // GET: Shelters/Search
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchResults(string microchip)
        {
            var shelter = await _context.Shelters.FirstOrDefaultAsync(s => s.Microchip == microchip);
            if (shelter == null)
            {
                ViewBag.Message = "Animal not found.";
                return View();
            }
            return View(shelter);
        }

        // GET: Shelters/Active
        public IActionResult Active()
        {
            return View();
        }

        // GET: Shelters/GetActiveShelters
        public async Task<IActionResult> GetActiveShelters(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Shelters.AsQueryable();

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(s =>
                    (s.AdmissionDate >= startDate.Value && s.AdmissionDate <= endDate.Value) ||
                    (s.DischargeDate.HasValue && s.DischargeDate.Value >= startDate.Value && s.DischargeDate.Value <= endDate.Value) ||
                    (s.AdmissionDate <= endDate.Value && (s.DischargeDate == null || s.DischargeDate >= startDate.Value)));
            }

            var activeShelters = await query
                .Select(s => new
                {
                    Name = s.Name,
                    Type = s.Type,
                    CoatColor = s.CoatColor,
                    AdmissionDate = s.AdmissionDate,
                    DischargeDate = s.DischargeDate,
                    PhotoUrl = s.PhotoUrl
                })
                .ToListAsync();

            return Json(activeShelters);
        }
    }
}

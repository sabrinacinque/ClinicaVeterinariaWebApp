﻿using ClinicaVeterinariaWebApp.Data;
using ClinicaVeterinariaWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AnimalsController : Controller
{
    private readonly VeterinaryContext _context;

    public AnimalsController(VeterinaryContext context)
    {
        _context = context;
    }

    // GET: Animals
    public async Task<IActionResult> Index()
    {
        return View(await _context.Animals.ToListAsync());
    }

    // GET: Animals/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var animal = await _context.Animals
            .FirstOrDefaultAsync(m => m.Id == id);
        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }

    // GET: Animals/Create
    public IActionResult Create()
    {
        ViewBag.AnimalTypes = new SelectList(new[] { "Cane", "Gatto", "Cavallo", "Coniglio", "Criceto", "Altro" });
        return View();
    }

    // POST: Animals/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,RegistrationDate,Name,Type,CoatColor,BirthDate,Microchip,OwnerFirstName,OwnerLastName")] Animal animal)
    {
        if (ModelState.IsValid)
        {
            _context.Add(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.AnimalTypes = new SelectList(new[] { "Cane", "Gatto", "Cavallo", "Coniglio", "Criceto", "Altro" });
        return View(animal);
    }

    // GET: Animals/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var animal = await _context.Animals.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }
        ViewBag.AnimalTypes = new SelectList(new[] { "Cane", "Gatto", "Cavallo", "Coniglio", "Criceto", "Altro" });
        return View(animal);
    }

    // POST: Animals/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,RegistrationDate,Name,Type,CoatColor,BirthDate,Microchip,OwnerFirstName,OwnerLastName")] Animal animal)
    {
        if (id != animal.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(animal);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(animal.Id))
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
        ViewBag.AnimalTypes = new SelectList(new[] { "Cane", "Gatto", "Cavallo", "Coniglio", "Criceto", "Altro" });
        return View(animal);
    }

    // GET: Animals/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var animal = await _context.Animals
            .FirstOrDefaultAsync(m => m.Id == id);
        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }

    // POST: Animals/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var animal = await _context.Animals.FindAsync(id);
        if (animal != null)
        {
            _context.Animals.Remove(animal);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AnimalExists(int id)
    {
        return _context.Animals.Any(e => e.Id == id);
    }

    // GET: Animals/VisitHistory/5
    public async Task<IActionResult> VisitHistory(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var visits = await _context.Visits
            .Where(v => v.AnimalId == id)
            .OrderByDescending(v => v.VisitDate)
            .ToListAsync();

        ViewBag.AnimalId = id;

        if (visits == null || visits.Count == 0)
        {
            ViewBag.Message = "Non ci sono visite pregresse per l'animale selezionato.";
        }

        return View(visits);
    }
}

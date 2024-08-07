using ClinicaVeterinariaWebApp.Data;
using ClinicaVeterinariaWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SalesController : Controller
{
    private readonly VeterinaryContext _context;

    public SalesController(VeterinaryContext context)
    {
        _context = context;
    }

    // GET: Sales
    public async Task<IActionResult> Index()
    {
        var sales = _context.Sales.Include(s => s.Product).ThenInclude(p => p.Supplier);
        return View(await sales.ToListAsync());
    }

    // GET: Sales/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sale = await _context.Sales
            .Include(s => s.Product)
            .ThenInclude(p => p.Supplier)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (sale == null)
        {
            return NotFound();
        }

        return View(sale);
    }

    // GET: Sales/Create
    public IActionResult Create()
    {
        ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
        return View();
    }

    // POST: Sales/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CustomerFiscalCode,ProductId,PrescriptionNumber,SaleDate")] Sale sale)
    {
        if (ModelState.IsValid)
        {
            _context.Add(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Products = new SelectList(_context.Products, "Id", "Name", sale.ProductId);
        return View(sale);
    }

    // GET: Sales/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sale = await _context.Sales.FindAsync(id);
        if (sale == null)
        {
            return NotFound();
        }
        ViewBag.Products = new SelectList(_context.Products, "Id", "Name", sale.ProductId);
        return View(sale);
    }

    // POST: Sales/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerFiscalCode,ProductId,PrescriptionNumber,SaleDate")] Sale sale)
    {
        if (id != sale.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(sale);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(sale.Id))
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
        ViewBag.Products = new SelectList(_context.Products, "Id", "Name", sale.ProductId);
        return View(sale);
    }

    // GET: Sales/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sale = await _context.Sales
            .Include(s => s.Product)
            .ThenInclude(p => p.Supplier)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (sale == null)
        {
            return NotFound();
        }

        return View(sale);
    }

    // POST: Sales/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var sale = await _context.Sales.FindAsync(id);
        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SaleExists(int id)
    {
        return _context.Sales.Any(e => e.Id == id);
    }
}

using ClinicaVeterinariaWebApp.Data;
using ClinicaVeterinariaWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class ProductsController : Controller
{
    private readonly VeterinaryContext _context;

    public ProductsController(VeterinaryContext context)
    {
        _context = context;
    }

    // GET: Products
    public async Task<IActionResult> Index()
    {
        var products = _context.Products.Include(p => p.Supplier).Include(p => p.Drawer).ThenInclude(d => d.Cabinet);
        return View(await products.ToListAsync());
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Supplier)
            .Include(p => p.Drawer)
            .ThenInclude(d => d.Cabinet)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: Products/Create
    public IActionResult Create()
    {
        ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name");
        ViewBag.Drawers = new SelectList(_context.Drawers.Include(d => d.Cabinet), "Id", "Number");
        ViewBag.ProductTypes = new SelectList(new[] { "Medicinale", "Alimentare", "Altro" });
        ViewBag.Uses = new SelectList(new[] { "Otiti", "Raffreddori", "Dermatiti", "Terapia del dolore", "Cibo secco", "Cibo umido", "Integratori", "Pulci e zecche" });
        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,SupplierId,DrawerId,ProductType,Uses")] Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
        ViewBag.Drawers = new SelectList(_context.Drawers.Include(d => d.Cabinet), "Id", "Number", product.DrawerId);
        ViewBag.ProductTypes = new SelectList(new[] { "Medicinale", "Alimentare", "Altro" }, product.ProductType);
        ViewBag.Uses = new SelectList(new[] { "Otiti", "Raffreddori", "Dermatiti", "Terapia del dolore", "Cibo secco", "Cibo umido", "Integratori", "Pulci e zecche" }, product.Uses);
        return View(product);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
        ViewBag.Drawers = new SelectList(_context.Drawers.Include(d => d.Cabinet), "Id", "Number", product.DrawerId);
        ViewBag.ProductTypes = new SelectList(new[] { "Medicinale", "Alimentare", "Altro" }, product.ProductType);
        ViewBag.Uses = new SelectList(new[] { "Otiti", "Raffreddori", "Dermatiti", "Terapia del dolore", "Cibo secco", "Cibo umido", "Integratori", "Pulci e zecche" }, product.Uses);
        return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SupplierId,DrawerId,ProductType,Uses")] Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
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
        ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
        ViewBag.Drawers = new SelectList(_context.Drawers.Include(d => d.Cabinet), "Id", "Number", product.DrawerId);
        ViewBag.ProductTypes = new SelectList(new[] { "Medicinale", "Alimentare", "Altro" }, product.ProductType);
        ViewBag.Uses = new SelectList(new[] { "Otiti", "Raffreddori", "Dermatiti", "Terapia del dolore", "Cibo secco", "Cibo umido", "Integratori", "Pulci e zecche" }, product.Uses);
        return View(product);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Supplier)
            .Include(p => p.Drawer)
            .ThenInclude(d => d.Cabinet)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}

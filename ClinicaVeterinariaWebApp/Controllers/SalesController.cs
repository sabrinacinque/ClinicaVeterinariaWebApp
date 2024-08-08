using ClinicaVeterinariaWebApp.Data;
using ClinicaVeterinariaWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
        ViewBag.Cabinets = new SelectList(_context.Cabinets, "Id", "Code");
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
        ViewBag.Cabinets = new SelectList(_context.Cabinets, "Id", "Code");
        return View(sale);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sale = await _context.Sales
            .Include(s => s.Product)
                .ThenInclude(p => p.Drawer)
                    .ThenInclude(d => d.Cabinet)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sale == null)
        {
            return NotFound();
        }

        var selectedCabinetId = sale.Product?.Drawer?.CabinetId;
        var selectedDrawerId = sale.Product?.DrawerId;

        var cabinets = await _context.Cabinets.ToListAsync();
        var drawers = selectedCabinetId.HasValue
            ? await _context.Drawers.Where(d => d.CabinetId == selectedCabinetId.Value).ToListAsync()
            : new List<Drawer>();
        var products = selectedDrawerId.HasValue
            ? await _context.Products.Where(p => p.DrawerId == selectedDrawerId.Value).ToListAsync()
            : new List<Product>();

        ViewBag.Cabinets = cabinets;
        ViewBag.Drawers = drawers;
        ViewBag.Products = products;

        return View(sale);
    }

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

        var selectedProduct = await _context.Products
            .Include(p => p.Drawer)
                .ThenInclude(d => d.Cabinet)
            .FirstOrDefaultAsync(p => p.Id == sale.ProductId);

        var selectedCabinetId = selectedProduct?.Drawer?.CabinetId;
        var selectedDrawerId = selectedProduct?.DrawerId;

        var cabinets = await _context.Cabinets.ToListAsync();
        var drawers = selectedCabinetId.HasValue
            ? await _context.Drawers.Where(d => d.CabinetId == selectedCabinetId.Value).ToListAsync()
            : new List<Drawer>();
        var products = selectedDrawerId.HasValue
            ? await _context.Products.Where(p => p.DrawerId == selectedDrawerId.Value).ToListAsync()
            : new List<Product>();

        ViewBag.Cabinets = cabinets;
        ViewBag.Drawers = drawers;
        ViewBag.Products = products;

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

    [HttpGet]
    public async Task<IActionResult> GetDrawersByCabinetId(int cabinetId)
    {
        var drawers = await _context.Drawers
            .Where(d => d.CabinetId == cabinetId)
            .Select(d => new { d.Id, d.Number })
            .ToListAsync();

        return Json(drawers);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByDrawerId(int drawerId)
    {
        var products = await _context.Products
            .Where(p => p.DrawerId == drawerId)
            .Select(p => new { p.Id, p.Name })
            .ToListAsync();

        return Json(products);
    }

    public IActionResult Report()
    {
        return View();
    }

    public async Task<IActionResult> GetSalesByDate(DateTime date)
    {
        var sales = await _context.Sales
            .Include(s => s.Product)
            .Where(s => s.SaleDate.Date == date.Date)
            .ToListAsync();

        return PartialView("_SalesByDatePartial", sales);
    }

    public async Task<IActionResult> GetSalesByCustomer(string fiscalCode)
    {
        var sales = await _context.Sales
            .Include(s => s.Product)
            .Where(s => s.CustomerFiscalCode == fiscalCode)
            .ToListAsync();

        return PartialView("_SalesByCustomerPartial", sales);
    }

}

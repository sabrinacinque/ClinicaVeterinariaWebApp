using ClinicaVeterinariaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinariaWebApp.Data
{
    public class VeterinaryContext : DbContext  
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Visit> Visits { get; set; }

        public DbSet<Shelter> Shelters { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Drawer> Drawers { get; set; }

        public VeterinaryContext(DbContextOptions<VeterinaryContext> options) : base(options)
        {
        }
    }
}

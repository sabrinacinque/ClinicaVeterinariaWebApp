using ClinicaVeterinariaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinariaWebApp.Data
{
    public class VeterinaryContext : DbContext  
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Visit> Visits { get; set; }

        public VeterinaryContext(DbContextOptions<VeterinaryContext> options) : base(options)
        {
        }
    }
}

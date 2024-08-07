using Microsoft.Build.Framework;

namespace ClinicaVeterinariaWebApp.Models
{
    public class Drawer
    {
        public int Id { get; set; }
        [Required]
        public int Number { get; set; } // Numero del cassetto
        [Required]
        public int  ? CabinetId { get; set; }
        public Cabinet ? Cabinet { get; set; }
        public ICollection<Product> ? Products { get; set; }
    }

}

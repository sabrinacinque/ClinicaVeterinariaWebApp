using Microsoft.Build.Framework;

namespace ClinicaVeterinariaWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProductType { get; set; } // Medicinali o Alimentari
        [Required]
        public string Uses { get; set; } // Elenco degli usi possibili
        [Required]
        public int ? SupplierId { get; set; }
        public Supplier ? Supplier { get; set; }
    }


}

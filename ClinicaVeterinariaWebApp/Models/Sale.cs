using Microsoft.Build.Framework;

namespace ClinicaVeterinariaWebApp.Models
{
    public class Sale
    {
        public int Id { get; set; }
        [Required]
        public string CustomerFiscalCode { get; set; }
        [Required]
        public int ? ProductId { get; set; }
        public Product ? Product { get; set; }
        public string PrescriptionNumber { get; set; } // Numero della ricetta medica
        [Required]
        public DateTime SaleDate { get; set; }
    }

}

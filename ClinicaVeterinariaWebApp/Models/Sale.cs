using Microsoft.Build.Framework;

namespace ClinicaVeterinariaWebApp.Models
{
    public class Sale
    {
        public int Id { get; set; }
       
        public string CustomerFiscalCode { get; set; }
        
        public int ? ProductId { get; set; }
        public Product ? Product { get; set; }
        public string PrescriptionNumber { get; set; } // Numero della ricetta medica
        
        public DateTime SaleDate { get; set; }
    }

}

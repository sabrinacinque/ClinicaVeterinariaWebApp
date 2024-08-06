using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ClinicaVeterinariaWebApp.Models
{
    public class Shelter
    {
        public int Id { get; set; }

        
        public DateTime RegistrationDate { get; set; }

        [Required]
        public string Name { get; set; }

        
        public string Type { get; set; }

        
        public string CoatColor { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string ? Microchip { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime ? AdmissionDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DischargeDate { get; set; }

        public string ? PhotoUrl { get; set; }

        [NotMapped]
        public IFormFile ? Photo { get; set; }
    }
}

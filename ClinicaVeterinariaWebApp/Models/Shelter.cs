using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ClinicaVeterinariaWebApp.Models
{
    public class Shelter
    {
        public int Id { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string CoatColor { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string ? Microchip { get; set; }

        [Required]
        public DateTime ? AdmissionDate { get; set; }

        public string ? PhotoUrl { get; set; }

        [NotMapped]
        public IFormFile ? Photo { get; set; }
    }
}

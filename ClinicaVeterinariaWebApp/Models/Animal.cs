namespace ClinicaVeterinariaWebApp.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string  Name { get; set; }
        public string  Type { get; set; }
        public string  CoatColor { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ? Microchip { get; set; }
        
        public string  OwnerFirstName { get; set; }
        public string  OwnerLastName { get; set; }

        public ICollection<Visit> ? Visits { get; set; }
    }

}

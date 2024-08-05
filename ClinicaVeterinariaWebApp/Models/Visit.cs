namespace ClinicaVeterinariaWebApp.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public string ObjectiveExam { get; set; }
        public string PrescribedTreatment { get; set; }

        public int AnimalId { get; set; }
        public Animal ? Animal { get; set; }
    }

}

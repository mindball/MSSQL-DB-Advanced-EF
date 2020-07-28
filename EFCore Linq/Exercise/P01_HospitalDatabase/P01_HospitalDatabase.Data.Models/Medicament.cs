using System.Collections.Generic;

namespace P01_HospitalDatabase.Data.Models
{
    public class Medicament
    {
        public Medicament()
        {
            this.PatientMedicaments = new HashSet<PatientMedicaments>();
        }
        public int MedicamentId { get; set; }

        public string Name { get; set; }

        public ICollection<PatientMedicaments> PatientMedicaments { get; set; }

    }
}

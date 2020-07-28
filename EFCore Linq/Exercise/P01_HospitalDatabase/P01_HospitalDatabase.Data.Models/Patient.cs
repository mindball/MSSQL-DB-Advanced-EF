using System.Collections.Generic;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            this.Prescriptions = new HashSet<PatientMedicaments>();
            this.Diagnoses = new HashSet<Diagnose>();
            this.Visitations = new HashSet<Visitation>();
        }
        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public bool HasInsurance { get; set; }

        public virtual ICollection<PatientMedicaments> Prescriptions { get; set; }
               
        public virtual ICollection<Diagnose> Diagnoses { get; set; }
               
        public virtual ICollection<Visitation> Visitations { get; set; }
    }
}

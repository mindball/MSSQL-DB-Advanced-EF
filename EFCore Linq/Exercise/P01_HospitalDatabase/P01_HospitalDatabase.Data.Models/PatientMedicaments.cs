﻿namespace P01_HospitalDatabase.Data.Models
{
    public class PatientMedicaments
    {
        public int PatientMedicamentId { get; set; }

        public int PationId { get; set; }
        public Patient Patient { get; set; }

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }
    }
}

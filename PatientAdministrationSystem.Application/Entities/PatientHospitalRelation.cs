using System;

namespace PatientAdministrationSystem.Application.Entities
{
    public class PatientHospitalRelation
    {
        // The ID of the related patient
        public Guid PatientId { get; set; }

        // The ID of the related hospital
        public Guid HospitalId { get; set; }

        // The ID of the associated visit
        public Guid VisitId { get; set; }

        // Navigation property for the patient
        public PatientEntity Patient { get; set; } = null!;

        // Navigation property for the hospital
        public HospitalEntity Hospital { get; set; } = null!;

        // Navigation property for the visit
        public VisitEntity Visit { get; set; } = null!;
    }
}

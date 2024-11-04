using System;
using System.Collections.Generic;

namespace PatientAdministrationSystem.Application.Entities
{
    public class PatientEntity : Entity<Guid>
    {
        // First name of the patient (required)
        public string FirstName { get; set; } = string.Empty;

        // Last name of the patient (required)
        public string LastName { get; set; } = string.Empty;

        // Email address of the patient (required)
        public string Email { get; set; } = string.Empty;

        // Relationships to PatientHospitalRelation (optional collection)
        public ICollection<PatientHospitalRelation> PatientHospitals { get; set; } = new List<PatientHospitalRelation>();
    }
}

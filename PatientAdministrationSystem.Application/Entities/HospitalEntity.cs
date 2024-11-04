using System;
using System.Collections.Generic;

namespace PatientAdministrationSystem.Application.Entities
{
    public class HospitalEntity : Entity<Guid>
    {
        // Name of the hospital (required)
        public string Name { get; set; } = string.Empty;

        // Relationships to PatientHospitalRelation (optional collection)
        public ICollection<PatientHospitalRelation> PatientHospitals { get; set; } = new List<PatientHospitalRelation>();
    }
}

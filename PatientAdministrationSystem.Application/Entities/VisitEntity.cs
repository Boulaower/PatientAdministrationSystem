using System;
using System.Collections.Generic;

namespace PatientAdministrationSystem.Application.Entities
{
    public class VisitEntity : Entity<Guid>
    {
        // Date of the visit
        public DateTime Date { get; set; }

        // Reason for the visit (assumption)
        public string ReasonForVisit { get; set; } = string.Empty;

        // Relationships to PatientHospitalRelation (optional collection)
        public ICollection<PatientHospitalRelation> PatientHospitals { get; set; } = new List<PatientHospitalRelation>();
    }
}

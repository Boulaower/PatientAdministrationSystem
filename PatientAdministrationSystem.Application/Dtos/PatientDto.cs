using System;

namespace PatientAdministrationSystem.Application.Dtos
{
    public class PatientDto
    {
        /// <summary>
        /// Unique identifier for the patient
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The patient's full name
        /// </summary>
        public string Name { get; set; } = string.Empty; // Default to empty string to avoid null references

        /// <summary>
        /// The date of the patient's visit
        /// </summary>
        public DateTime? VisitDate { get; set; } // Changed to nullable DateTime

        /// <summary>
        /// The reason for the patient's visit
        /// </summary>
        public string ReasonForVisit { get; set; } = string.Empty; // Default to empty string to avoid null references
    }
}

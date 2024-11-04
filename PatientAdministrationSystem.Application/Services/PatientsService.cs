using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientAdministrationSystem.Application.Dtos;
using PatientAdministrationSystem.Application.Entities;
using PatientAdministrationSystem.Application.Interfaces;
using PatientAdministrationSystem.Application.Repositories.Interfaces;

namespace PatientAdministrationSystem.Application.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _repository;

        public PatientsService(IPatientsRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all patients.
        /// </summary>
        /// <returns>A list of PatientDto</returns>
        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _repository.GetAllAsync();

            // Map entities to DTOs
            return patients.Select(p => new PatientDto
            {
                Id = p.Id,
                Name = $"{p.FirstName} {p.LastName}",
                VisitDate = p.PatientHospitals?.FirstOrDefault()?.Visit?.Date ?? DateTime.MinValue,
                ReasonForVisit = p.PatientHospitals?.FirstOrDefault()?.Visit?.ReasonForVisit ?? string.Empty
            });
        }

        /// <summary>
        /// Retrieves a patient by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the patient to retrieve.</param>
        /// <returns>A PatientDto if found, throws exception otherwise</returns>
        public async Task<PatientDto> GetPatientByIdAsync(Guid id)
        {
            var patient = await _repository.GetByIdAsync(id);

            // Throw exception if the patient is not found to match non-nullable return type
            if (patient == null)
                throw new InvalidOperationException("Patient not found.");

            // Map entity to DTO
            return new PatientDto
            {
                Id = patient.Id,
                Name = $"{patient.FirstName} {patient.LastName}",
                VisitDate = patient.PatientHospitals?.FirstOrDefault()?.Visit?.Date ?? DateTime.MinValue,
                ReasonForVisit = patient.PatientHospitals?.FirstOrDefault()?.Visit?.ReasonForVisit ?? string.Empty
            };
        }

        /// <summary>
        /// Searches patients by their name.
        /// </summary>
        /// <param name="name">The name or part of the name to search for</param>
        /// <returns>A list of matching PatientDto</returns>
        public async Task<IEnumerable<PatientDto>> SearchPatientsByNameAsync(string name)
        {
            // Assuming repository supports querying based on conditions
            var patients = await _repository.FindAsync(p => (p.FirstName + " " + p.LastName).Contains(name));

            // Map entities to DTOs
            return patients.Select(p => new PatientDto
            {
                Id = p.Id,
                Name = $"{p.FirstName} {p.LastName}",
                VisitDate = p.PatientHospitals?.FirstOrDefault()?.Visit?.Date ?? DateTime.MinValue,
                ReasonForVisit = p.PatientHospitals?.FirstOrDefault()?.Visit?.ReasonForVisit ?? string.Empty
            });
        }

        /// <summary>
        /// Creates a new patient.
        /// </summary>
        /// <param name="patientDto">The PatientDto containing the data to create</param>
        /// <returns>The created PatientDto</returns>
        public async Task<PatientDto> CreatePatientAsync(PatientDto patientDto)
        {
            // Split name into first and last names
            var nameParts = (patientDto.Name ?? string.Empty).Split(' ', 2);
            var firstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
            var lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;

            var newPatient = new PatientEntity
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName
            };

            var createdPatient = await _repository.AddAsync(newPatient);
            return new PatientDto
            {
                Id = createdPatient.Id,
                Name = $"{createdPatient.FirstName} {createdPatient.LastName}"
            };
        }

        /// <summary>
        /// Updates an existing patient's details.
        /// </summary>
        /// <param name="id">The ID of the patient to update</param>
        /// <param name="patientDto">The updated patient data</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        public async Task<bool> UpdatePatientAsync(Guid id, PatientDto patientDto)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null)
                return false;

            // Split name into first and last names
            var nameParts = (patientDto.Name ?? string.Empty).Split(' ', 2);
            patient.FirstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
            patient.LastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;

            return await _repository.UpdateAsync(patient);
        }

        /// <summary>
        /// Deletes a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to delete</param>
        /// <returns>True if the patient was successfully deleted, false otherwise</returns>
        public async Task<bool> DeletePatientAsync(Guid id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null)
                return false;

            return await _repository.DeleteAsync(patient);
        }
    }
}

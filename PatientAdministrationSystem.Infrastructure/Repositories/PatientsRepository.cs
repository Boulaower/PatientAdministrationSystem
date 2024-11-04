using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PatientAdministrationSystem.Application.Entities;
using PatientAdministrationSystem.Application.Repositories.Interfaces;
using PatientAdministrationSystem.Infrastructure;

namespace PatientAdministrationSystem.Infrastructure.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly HciDataContext _context;

        public PatientsRepository(HciDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all patients from the database.
        /// </summary>
        /// <returns>A list of all PatientEntity objects</returns>
        public async Task<IEnumerable<PatientEntity>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        /// <summary>
        /// Finds patients based on a specified condition.
        /// </summary>
        /// <param name="predicate">Condition to filter patients</param>
        /// <returns>A list of patients matching the condition</returns>
        public async Task<IEnumerable<PatientEntity>> FindAsync(Expression<Func<PatientEntity, bool>> predicate)
        {
            return await _context.Patients.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Retrieves a patient by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the patient to retrieve</param>
        /// <returns>A PatientEntity if found, null otherwise</returns>
        public async Task<PatientEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Patients.FindAsync(id);
        }

        /// <summary>
        /// Adds a new patient to the database.
        /// </summary>
        /// <param name="patient">The PatientEntity to add</param>
        /// <returns>The added PatientEntity</returns>
        public async Task<PatientEntity> AddAsync(PatientEntity patient)
        {
            // Ensure the patient has a unique ID
            if (await _context.Patients.AnyAsync(p => p.Id == patient.Id))
                throw new InvalidOperationException("A patient with the same ID already exists.");

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        /// <summary>
        /// Updates an existing patient in the database.
        /// </summary>
        /// <param name="patient">The updated PatientEntity</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        public async Task<bool> UpdateAsync(PatientEntity patient)
        {
            var existingPatient = await _context.Patients.FindAsync(patient.Id);
            if (existingPatient == null)
                return false;

            // Update patient properties
            _context.Entry(existingPatient).CurrentValues.SetValues(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Removes a patient from the database.
        /// </summary>
        /// <param name="patient">The PatientEntity to remove</param>
        /// <returns>True if the deletion was successful, false otherwise</returns>
        public async Task<bool> DeleteAsync(PatientEntity patient)
        {
            var existingPatient = await _context.Patients.FindAsync(patient.Id);
            if (existingPatient == null)
                return false;

            _context.Patients.Remove(existingPatient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

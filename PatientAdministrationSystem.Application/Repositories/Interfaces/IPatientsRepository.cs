// Import necessary namespaces for the interface
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PatientAdministrationSystem.Application.Entities;

namespace PatientAdministrationSystem.Application.Repositories.Interfaces
{
    // Define the interface for the PatientsRepository
    public interface IPatientsRepository
    {
        // Asynchronously retrieves all PatientEntity records from the data source
        Task<IEnumerable<PatientEntity>> GetAllAsync();
        
        // Asynchronously finds PatientEntity records that match a given condition (predicate)
        Task<IEnumerable<PatientEntity>> FindAsync(Expression<Func<PatientEntity, bool>> predicate);
        
        // Asynchronously retrieves a single PatientEntity record by its unique ID
        Task<PatientEntity?> GetByIdAsync(Guid id);
        
        // Asynchronously adds a new PatientEntity record to the data source
        Task<PatientEntity> AddAsync(PatientEntity patient);
        
        // Asynchronously updates an existing PatientEntity record in the data source
        Task<bool> UpdateAsync(PatientEntity patient);
        
        // Asynchronously deletes a specified PatientEntity record from the data source
        Task<bool> DeleteAsync(PatientEntity patient);
    }
}

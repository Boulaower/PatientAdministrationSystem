using System.Collections.Generic;
using System.Threading.Tasks;
using PatientAdministrationSystem.Application.Dtos;

namespace PatientAdministrationSystem.Application.Interfaces
{
    public interface IPatientsService
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<IEnumerable<PatientDto>> SearchPatientsByNameAsync(string name);
        Task<PatientDto> GetPatientByIdAsync(Guid id); // Changed to Guid
        Task<PatientDto> CreatePatientAsync(PatientDto patientDto);
        Task<bool> UpdatePatientAsync(Guid id, PatientDto patientDto); // Changed to Guid
        Task<bool> DeletePatientAsync(Guid id); // Changed to Guid
    }
}

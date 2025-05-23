using PharmacyApp.Data;
using PharmacyApp.DTOs;

namespace PharmacyApp.Services;

public interface IDbService
{
    public Task<PatientGetDto> GetPatientDetailsAsync(int id);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PatientGetDto> GetPatientDetailsAsync(int id)
    {
        var result = data.Patients.Select(p => new PatientGetDto
        {
            IdPatient = p.IdPatient,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Birthdate = p.BirthDate,
            

        });
        return null;
    }
}
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.DTOs;
using PharmacyApp.Exceptions;
using PharmacyApp.Models;

namespace PharmacyApp.Services;

public interface IDbService
{
    public Task<PatientGetDto> GetPatientDetailsAsync(int id);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<PatientGetDto> GetPatientDetailsAsync(int id)
    {
        var result = await data.Patients.Select(p => new PatientGetDto
        {
            IdPatient = p.IdPatient,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Birthdate = p.BirthDate,
            Prescriptions = p.Prescriptions.Select(pr => new PrescriptionsGetDto
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Medicament = pr.PrescriptionMedicaments.Select(pm => new MedicamentPrescriptionsGetDto
                {
                    IdMedicament = pm.IdMedicament,
                    Name = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Description = pm.Medicament.Description,
                    Type = pm.Medicament.Type
                }).ToList(),
                Doctor = new DoctorGetDto
                {
                    IdDoctor = pr.Doctor.IdDoctor,
                    FirstName = pr.Doctor.FirstName,
                    LastName = pr.Doctor.LastName,
                    Email = pr.Doctor.Email
                }
            }).ToList()
        }).FirstOrDefaultAsync(e => e.IdPatient == id);
        return result ?? throw new NotFoundException($"Patient with id: {id} not found");
    }
}
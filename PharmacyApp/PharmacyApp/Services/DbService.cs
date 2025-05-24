using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.DTOs;
using PharmacyApp.Exceptions;
using PharmacyApp.Models;

namespace PharmacyApp.Services;

public interface IDbService
{
    public Task<PatientGetDto> GetPatientDetailsAsync(int id);
    public Task<PrescriptionCreateDto> CreateNewPrescriptionAsync(PrescriptionCreateDto pr);
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
            Birthdate = p.Birthdate,
            Prescriptions = p.Prescriptions.Select(pr => new PrescriptionGetDto
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Medicament = pr.PrescriptionMedicaments.Select(pm => new MedicamentPrescriptionGetDto
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

    public async Task<PrescriptionCreateDto> CreateNewPrescriptionAsync(PrescriptionCreateDto pr)
    {
        if (pr.Medicaments.Count > 10) 
            throw new MaxLimitReached("A prescription can include a maximum of 10 medicaments.");

        if (pr.DueDate < pr.Date)
            throw new DateValidationException("The DueDate must be greater than or equal to the Date.");

        var patient = await data.Patients
            .FirstOrDefaultAsync(p => p.IdPatient == pr.Patient.IdPatient);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = pr.Patient.FirstName,
                LastName = pr.Patient.LastName,
                Birthdate = pr.Patient.Birthdate
            };
            data.Patients.Add(patient);
            await data.SaveChangesAsync();
        }

        var doctor = await data.Doctors
            .FirstOrDefaultAsync(d => d.IdDoctor == pr.Doctor.IdDoctor);
        if (doctor == null)
            throw new NotFoundException("Doctor does not exist!");

        var prescription = new Prescription
        {
            Date = pr.Date,
            DueDate = pr.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor,
            PrescriptionMedicaments = new List<PrescriptionMedicament>()
        };

        foreach (var m in pr.Medicaments)
        {
            var medicament = await data.Medicament.FindAsync(m.IdMedicament);
            if (medicament == null)
                throw new NotFoundException($"Lek o ID {m.IdMedicament} nie istnieje.");

            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            });
        }

        data.Prescriptions.Add(prescription);
        await data.SaveChangesAsync();
        return new PrescriptionCreateDto
        {
            Patient = new PatientDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthdate = patient.Birthdate
            },
            Doctor = new DoctorDto
            {
                IdDoctor = doctor.IdDoctor
            },
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Medicaments = prescription.PrescriptionMedicaments.Select(pm => new MedicamentDto
            {
                IdMedicament = pm.IdMedicament,
                Dose = pm.Dose,
                Details = pm.Details
            }).ToList()
        };
    }
}
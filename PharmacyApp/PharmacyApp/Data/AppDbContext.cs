using Microsoft.EntityFrameworkCore;
using PharmacyApp.Models;

namespace PharmacyApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicament { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionsMedicament { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}
    
    //przykladowe dane
   protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Seed Doctors
    modelBuilder.Entity<Doctor>().HasData(
        new Doctor { IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
        new Doctor { IdDoctor = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" },
        new Doctor { IdDoctor = 3, FirstName = "Emily", LastName = "Johnson", Email = "emily.johnson@example.com" }
    );

    // Seed Patients
    modelBuilder.Entity<Patient>().HasData(
        new Patient { IdPatient = 1, FirstName = "Michael", LastName = "Brown", BirthDate = new DateTime(1985, 5, 20) },
        new Patient { IdPatient = 2, FirstName = "Sarah", LastName = "Davis", BirthDate = new DateTime(1990, 8, 15) },
        new Patient { IdPatient = 3, FirstName = "David", LastName = "Wilson", BirthDate = new DateTime(2000, 3, 10) }
    );

    // Seed Medicaments
    modelBuilder.Entity<Medicament>().HasData(
        new Medicament { IdMedicament = 1, Name = "Aspirin", Description = "Pain reliever", Type = "Tablet" },
        new Medicament { IdMedicament = 2, Name = "Ibuprofen", Description = "Anti-inflammatory", Type = "Capsule" },
        new Medicament { IdMedicament = 3, Name = "Paracetamol", Description = "Fever reducer", Type = "Syrup" }
    );

    // Seed Prescriptions
    modelBuilder.Entity<Prescription>().HasData(
        new Prescription { IdPrescription = 1, Date = new DateTime(2023, 10, 1), DueDate = new DateTime(2023, 10, 8), IdPatient = 1, IdDoctor = 1 },
        new Prescription { IdPrescription = 2, Date = new DateTime(2023, 10, 2), DueDate = new DateTime(2023, 10, 12), IdPatient = 2, IdDoctor = 2 },
        new Prescription { IdPrescription = 3, Date = new DateTime(2023, 10, 3), DueDate = new DateTime(2023, 10, 8), IdPatient = 3, IdDoctor = 3 }
    );

    // Seed PrescriptionMedicaments
    modelBuilder.Entity<PrescriptionMedicament>().HasData(
        new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 2, Details = "Take after meals" },
        new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 2, Dose = 1, Details = "Take before bed" },
        new PrescriptionMedicament { IdMedicament = 3, IdPrescription = 3, Dose = 3, Details = "Take every 6 hours" },
    new PrescriptionMedicament { IdMedicament = 3, IdPrescription = 1, Dose = 3, Details = "Take every 6 hours" }
    );
}
}
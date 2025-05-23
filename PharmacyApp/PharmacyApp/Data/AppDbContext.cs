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
    
    
}
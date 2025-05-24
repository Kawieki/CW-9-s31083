using PharmacyApp.Models;

namespace PharmacyApp.DTOs;

public class PrescriptionGetDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorGetDto Doctor { get; set; }
    public ICollection<MedicamentPrescriptionGetDto> Medicament { get; set; }
}
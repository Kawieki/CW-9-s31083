using PharmacyApp.Models;

namespace PharmacyApp.DTOs;

public class PrescriptionsGetDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorGetDto Doctor { get; set; }
    public ICollection<MedicamentPrescriptionsGetDto> Medicament { get; set; }
}
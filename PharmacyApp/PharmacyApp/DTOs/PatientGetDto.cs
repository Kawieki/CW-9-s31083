using PharmacyApp.Models;

namespace PharmacyApp.DTOs;

public class PatientGetDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public virtual ICollection<PrescriptionGetDto> Prescriptions { get; set; }
    
}
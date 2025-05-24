namespace PharmacyApp.DTOs;

public class PrescriptionCreateDto
{
    public PatientDto Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
}

public class PatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}

public class DoctorDto
{
    public int IdDoctor { get; set; }
}

public class MedicamentDto
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}
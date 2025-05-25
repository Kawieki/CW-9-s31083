using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.DTOs;

public class PrescriptionCreateDto
{
    [Required]
    public PatientDto Patient { get; set; }
    
    [Required]
    public DoctorDto Doctor { get; set; }
   
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    [Required]
    public List<MedicamentDto> Medicaments { get; set; }
}

public class PatientDto
{
    [Required]
    public int IdPatient { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    public DateTime Birthdate { get; set; }
}

public class DoctorDto
{
    [Required]
    public int IdDoctor { get; set; }
}

public class MedicamentDto
{
    public MedicamentDto()
    {
    }

    public MedicamentDto(int idMedicament, int? dose, string details)
    {
        IdMedicament = idMedicament;
        Dose = dose;
        Details = details;
    }

    [Required]
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PharmacyApp.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class PrescriptionMedicament
{
    
    public int IdMedicament { get; set; } 
    public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    
    [Required]
    [MaxLength(100)]
    public int Details { get; set; }
}
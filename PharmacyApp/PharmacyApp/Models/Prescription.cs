using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyApp.Models;
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    public DateTime Date { get; set; }
    public DateTime DueTime { get; set; }
    
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }

    [ForeignKey(nameof(IdPatient))]
    public virtual Patient Patient { get; set; }
    
    [ForeignKey(nameof(IdDoctor))]
    public virtual Doctor Doctor { get; set; }

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}
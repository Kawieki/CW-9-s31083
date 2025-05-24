namespace PharmacyApp.DTOs;

public class MedicamentPrescriptionGetDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
}
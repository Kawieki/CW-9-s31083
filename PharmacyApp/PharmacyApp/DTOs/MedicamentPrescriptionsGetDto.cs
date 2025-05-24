namespace PharmacyApp.DTOs;

public class MedicamentPrescriptionsGetDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
}
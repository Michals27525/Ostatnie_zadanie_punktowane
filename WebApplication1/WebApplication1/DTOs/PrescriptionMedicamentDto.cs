namespace WebApplication1.DTOs;

public class PrescriptionMedicamentDto
{
    public MedicamentDto Medicament { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; } = null!;
    
    
}
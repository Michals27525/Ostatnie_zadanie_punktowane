namespace WebApplication1.DTOs;

public class PrescriptionDto
{
    
    public int IdPrescription { get; set; }

    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public DoctorGetDto Doctor { get; set; } = null!;
    
    public ICollection<PrescriptionMedicamentDto> Medicaments { get; set; }

}
namespace WebApplication1.DTOs;

public class PatientGetDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; }= null!;
    public DateTime Birthdate { get; set; }
    
    public ICollection<PrescriptionDto> Prescriptions { get; set; }
}


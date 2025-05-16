namespace WebApplication1.DTOs;

public class PrescriptionCreateReqDto
{

    public PatientCreateReqDto Patient { get; set; } = null!;
    public ICollection<MedicamentCreateReqDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
    
    
    
}

public class PatientCreateReqDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; }= null!;
    public DateTime Birthdate { get; set; }
}

public class MedicamentCreateReqDto
{
    public int  IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; } = null!;

}



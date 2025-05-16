namespace WebApplication1.DTOs;

public class DoctorGetDto
{
  
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public ICollection<DoctorGetDtoPrescription> Prescriptions { get; set; }
    //public ICollection<PrescriptionDto> Prescriptions { get; set; }
    
}

public class DoctorGetDtoPrescription
{
    public int IdPrescription { get; set; }

    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
}

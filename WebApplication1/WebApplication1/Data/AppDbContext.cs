using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDbContext: DbContext
{

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var doctor = new Doctor()
        {
            IdDoctor = 1,
            FirstName = "Alan",
            LastName = "Doe",
            Email = "mail@email.com",
        };

        var patient = new Patient()
        {
            IdPatient = 1,
            FirstName = "Max",
            LastName = "Kowalsky",
            Birthdate = new DateTime(1990,3,3)
        };

        var medicament = new Medicament()
        {   
            IdMedicament = 1,
            Name = "Mocny proch",
            Description = "Mocno kopie, nie podawac dzieciom",
            Type = "prochy"
        };

        var prescription = new Prescription()
        {
            IdPrescription = 1,
            Date = new DateTime(2025,05,15),
            DueDate = new DateTime(2025,12,12),
            IdPatient = 1,
            IdDoctor = 1
            
        };

        var prescriptionMedicament = new PrescriptionMedicament()
        {
            IdPrescription = 1,
            IdMedicament = 1,
            Dose = null,
            Details = "jakies cos nie wiem"
        };


        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Medicament>().HasData(medicament);
        modelBuilder.Entity<Prescription>().HasData(prescription);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicament);
    }
    
    
}
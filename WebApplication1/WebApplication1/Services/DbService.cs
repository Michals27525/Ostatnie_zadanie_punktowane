using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IDbService
{
    public Task<ICollection<DoctorGetDto>> GetDoctosDetailsAsync();
    public Task<ICollection<PatientGetDto>> GetPatientDetailsAsync();
    public Task<PatientGetDto?> GetPatientByIdAsync(int id);
    public Task AddPrescriptionAsync(PrescriptionCreateReqDto request);

}


public class DbService(AppDbContext data):IDbService
{
    public async Task<ICollection<DoctorGetDto>> GetDoctosDetailsAsync()
    {
        return await data.Doctors.Select(d=>new DoctorGetDto()
        {
            IdDoctor = d.IdDoctor,
            FirstName = d.FirstName,
            LastName = d.LastName,
            Email = d.Email,
            Prescriptions = d.Prescriptions.Select(dd=>new DoctorGetDtoPrescription()
            {
                IdPrescription = dd.IdPrescription,
                Date = dd.Date,
                DueDate = dd.DueDate
                
            }).ToList()
            
        }).ToListAsync();
    }

    public async Task<ICollection<PatientGetDto>> GetPatientDetailsAsync()
    {
        return await data.Patients.Select(p => new PatientGetDto()
        {
            IdPatient = p.IdPatient,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Birthdate = p.Birthdate,
            Prescriptions = p.Prescriptions.Select(pr=>new PrescriptionDto
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate
            }).ToList()
        }).ToListAsync();
    }

    public async Task<PatientGetDto?> GetPatientByIdAsync(int id)
    {
        return await data.Patients
            .Where(p => p.IdPatient == id)
            .Select(pp => new PatientGetDto()
            {
                IdPatient = pp.IdPatient,
                FirstName = pp.FirstName,
                LastName = pp.LastName,
                Birthdate = pp.Birthdate,
                Prescriptions = pp.Prescriptions
                    .OrderBy(o=>o.DueDate)
                    .Select(o=> new PrescriptionDto()
                    {
                        IdPrescription = o.IdPrescription,
                        Date = o.Date,
                        DueDate = o.DueDate,
                        Doctor = new DoctorGetDto()
                        {
                            IdDoctor = o.Doctor.IdDoctor,
                            FirstName = o.Doctor.FirstName,
                            LastName = o.Doctor.LastName,
                            Email = o.Doctor.Email
                        },
                        Medicaments = o.PrescriptionMedicaments
                            .Select(pm=> new PrescriptionMedicamentDto()
                            {
                                Medicament = new MedicamentDto()
                                {
                                    IdMedicament = pm.Medicament.IdMedicament,
                                    Name=pm.Medicament.Name,
                                    Description = pm.Medicament.Description,
                                    Type = pm.Medicament.Type
                                },
                                Dose = pm.Dose,
                                Details = pm.Details
                            }).ToList()
                    }).ToList()
            }).FirstOrDefaultAsync();

    }


    public async Task AddPrescriptionAsync(PrescriptionCreateReqDto request)
    {
        if (request.Medicaments.Count>10)
        {
            throw new Exception("Nie może być więcej niż 10 leków");
        }

        if (request.DueDate<request.Date)
        {
            throw new Exception("DueDate < Date tak być nie może");
        }
        
        var medicamentsFromReq = request.Medicaments
            .Select(m => m.IdMedicament).ToList();

        var medicamentsFromDb = await data.Medicaments
            .Where(m=>medicamentsFromReq.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament).ToListAsync();
        var missing = medicamentsFromReq.Except(medicamentsFromDb).ToList();

        if (missing.Any())
        {
            throw new Exception($"Nie znaleziono leków: {string.Join(", ",missing)}");
        }

        
        var Doctor = await data.Doctors.FirstOrDefaultAsync(d => request.IdDoctor == d.IdDoctor);
        if (Doctor==null)
        {
            throw new Exception($"Lekarz o id {request.IdDoctor} nie istnieje");
        }

        
        Patient? patient=await data.Patients.FindAsync(request.Patient.IdPatient);
        if (patient==null)
        {
            patient = new Patient()
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };

            await data.Patients.AddAsync(patient);
            await data.SaveChangesAsync();
        }

        Prescription prescription = new Prescription()
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = Doctor.IdDoctor,
            PrescriptionMedicaments = request.Medicaments
                .Select(m=>new PrescriptionMedicament()
                {
                    IdMedicament = m.IdMedicament,
                    Details = m.Description,
                    Dose = m.Dose
                }).ToList()
        };

        await data.Prescriptions.AddAsync(prescription);
        await data.SaveChangesAsync();



    }

}
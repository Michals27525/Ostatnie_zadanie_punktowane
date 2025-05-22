using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class PatientController(IDbService service) :ControllerBase
{

    [HttpGet]//http://localhost:5097/patient
    public async Task<IActionResult> GetPatientDetails()
    {
        return Ok(await service.GetPatientDetailsAsync());
    }

    [HttpGet("{id}")] //http://localhost:5097/patient/1
    public async Task<IActionResult> GetPatientById(int id)
    {
        var patient = await service.GetPatientByIdAsync(id);
        if (patient == null)
        {
            return NotFound($"Pacjent z id{id} nie istnieje");
        }

        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateReqDto reqDto)
    {
        try
        {
            await service.AddPrescriptionAsync(reqDto);
            return Created("",null);
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    //testowy POST - nowy pacjent
    /*{
        "patient": {
            "idPatient": 0,
            "firstName": "Jan",
            "lastName": "fajny",
            "birthdate": "1980-05-12"
        },
        "medicaments": [
        {
            "idMedicament": 1,
            "dose": 3,
            "description": "Podawac po kazdym posilku"
        }
        ],
        "date": "2024-01-01",
        "dueDate": "2024-02-01",
        "idDoctor": 1
    }
    */
    

}















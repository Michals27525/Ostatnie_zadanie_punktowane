using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;


[ApiController]
[Route("[controller]")]
public class PatientController(IDbService service) :ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetPatientDetails()
    {
        return Ok(await service.GetPatientDetailsAsync());
    }

    [HttpGet("{id}")]
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
            return Created("udalo sie",null);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}















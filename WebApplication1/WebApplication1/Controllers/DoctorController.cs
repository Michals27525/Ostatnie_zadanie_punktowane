using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class DoctorController(IDbService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetStudentDetails()
    {
        
        return Ok(await service.GetDoctosDetailsAsync());
    }
    
    



}
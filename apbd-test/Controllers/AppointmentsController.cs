using Microsoft.AspNetCore.Mvc;
using Test1.Services;

namespace Test1;


[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        var appointment = _appointmentService.GetByIdAsync(Id);
        if (appointment == null) return NotFound();
        
        return Ok(appointment);
    }
    
    //[HttpPost]
}
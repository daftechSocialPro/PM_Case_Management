using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Services.CaseMGMT.AppointmentService;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/case")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("appointment")]
        public async Task<IActionResult> Create(AppointmentPostDto appointmentPostDto)
        {
            try
            {
                await _appointmentService.AddAppointment(appointmentPostDto);
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("appointment")]
        public async Task<IActionResult> GetAll()
        {
            try
            { 
                List<Appointement> appointements = await _appointmentService.GetAllAppointments();
                return Ok(appointements);
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

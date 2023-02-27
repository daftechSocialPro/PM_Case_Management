using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Services.CaseMGMT.AppointmentWithCalenderService;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/case")]
    [ApiController]
    public class AppointmentWithCalenderController : ControllerBase
    {
        private readonly IAppointmentWithCalenderService _appointmentWithCalenderService;

        public AppointmentWithCalenderController(IAppointmentWithCalenderService appointmentWithCalenderService)
        {
            _appointmentWithCalenderService = appointmentWithCalenderService;
        }

        [HttpGet("appointmetWithCalender")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _appointmentWithCalenderService.GetAll());
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("appointmetWithCalender")]
        public async Task<IActionResult> Create(AppointmentWithCalenderPostDto appointmentWithCalender)
        {
            try
            {
                await _appointmentWithCalenderService.Add(appointmentWithCalender);
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Services.CaseMGMT.Applicants;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/case")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {

        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpGet("applicant")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _applicantService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("applicantSelectList")]
        public async Task<IActionResult> GetSelectAll()
        {
            try
            {
                return Ok(await _applicantService.GetSelectList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost("applicant")]
        public async Task<IActionResult> Create(ApplicantPostDto applicantPostDto)
        {
            try
            {
                await _applicantService.Add(applicantPostDto);
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

      
    }
}

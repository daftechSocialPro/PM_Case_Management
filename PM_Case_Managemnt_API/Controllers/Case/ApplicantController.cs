﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.Case;
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

        [HttpPost("applicant")]
        public async Task<IActionResult> Create(ApplicantPostDto applicantPostDto)
        {
            try
            {
                await _applicantService.AddApplicant(applicantPostDto);
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("applicant")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<ApplicantGetDto> applicants = await _applicantService.GetAll();
                return Ok(applicants);
            } catch(Exception ex) {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

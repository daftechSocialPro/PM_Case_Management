﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.Services.PM;
using PM_Case_Managemnt_API.Services.PM.ProgressReport;

namespace PM_Case_Managemnt_API.Controllers.PM
{
    [Route("api/PM/[controller]")]
    [ApiController]
    public class ProgressReportController : ControllerBase
    {

        private readonly IProgressReportService _progressReportService;
        public ProgressReportController(IProgressReportService progressReportService)
        {
            _progressReportService = progressReportService;
        }


        [HttpGet("DirectorLevelPerformance")]
        public async Task<IActionResult> GetOrganizationDiaram(Guid? BranchId)
        {
            try
            {
                return Ok(await _progressReportService.GetDirectorLevelPerformance(BranchId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("ProgramBudgetReport")]
        public async Task<IActionResult> ProgramBudgetReport(string BudgetYear, string ReportBy)
        {
            try
            {
                return Ok(await _progressReportService.PlanReportByProgram(BudgetYear, ReportBy));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

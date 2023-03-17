using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Case;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Services.CaseMGMT;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/case/[controller]")]
    [ApiController]
    public class CaseREportController : ControllerBase
    {
        private readonly ICaseReportService _caserReportService;
        private readonly DBContext _dbContext;
        public CaseREportController(ICaseReportService caseReportService, DBContext dBContext)
        {
            _caserReportService = caseReportService;
            _dbContext = dBContext;
        }


        [HttpGet("GetCaseReport")]

        public async Task<IActionResult> GetCaseReport(string? startAt, string? endAt)
        {

            try
            {
                return Ok(await _caserReportService.GetCaseReport(startAt, endAt));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet("GetCasePieChart")]

        public async Task<IActionResult> GetCasePieChart(string? startAt, string? endAt)
        {

            try
            {
                return Ok(await _caserReportService.GetCasePieChart(startAt, endAt));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }




        }

        [HttpGet("GetCasePieChartByStatus")]

        public async Task<IActionResult> GetCasePieCharByCaseStatus(string? startAt, string? endAt)
        {
            try
            {
                return Ok(await _caserReportService.GetCasePieCharByCaseStatus(startAt, endAt));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.Data;
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

        public async Task<IActionResult> GetCaseReport()
        {

            try
            {
                return Ok(await _caserReportService.GetCaseReport());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }
    }
}

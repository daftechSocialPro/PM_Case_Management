using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Services.CaseMGMT.History;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/case")]
    [ApiController]
    public class CaseHistoryController : ControllerBase
    {
        private readonly ICaseHistoryService _caseHistoryService;

        public CaseHistoryController(ICaseHistoryService caseHistoryService)
        {
            _caseHistoryService = caseHistoryService;
        }

        [HttpPost("setSeen")]
        public async Task<IActionResult> SetSeen(CaseHistorySeenDto seenDto)
        {
            try
            {
                await _caseHistoryService.SetCaseSeen(seenDto);
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPost("setComplete")]
        public async Task<IActionResult> SetComplete(CaseHistorySeenDto seenDto)
        {
            try
            {
                await _caseHistoryService.SetCaseSeen(seenDto);
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        
    }
}

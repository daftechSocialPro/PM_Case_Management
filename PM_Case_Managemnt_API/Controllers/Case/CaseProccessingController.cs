using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Services.CaseMGMT;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/case")]
    [ApiController]
    public class CaseProccessingController : ControllerBase
    {
        private readonly ICaseProccessingService _caseProcessingService; 
        public CaseProccessingController(ICaseProccessingService caseProccessingService)
        {
            _caseProcessingService= caseProccessingService;
        }


        [HttpPut("confirmcase")]

        public async Task<IActionResult> ConfirmCase(ConfirmTranscationDto confirmTranscationDto)
        {

            try
            {
                return Ok(await _caseProcessingService.ConfirmTranasaction(confirmTranscationDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet("getCaseDetail")]

        public async Task<IActionResult> GetCaseDetail(Guid EmployeeId,Guid CaseHistoryId)
        {
            try
            {
                return Ok(await _caseProcessingService.GetCaseDetial(EmployeeId, CaseHistoryId));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }




    }
}

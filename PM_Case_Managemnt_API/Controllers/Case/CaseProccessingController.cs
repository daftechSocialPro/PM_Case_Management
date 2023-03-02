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
            _caseProcessingService = caseProccessingService;
        }


        [HttpPut("confirm")]

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

        public async Task<IActionResult> GetCaseDetail(Guid EmployeeId, Guid CaseHistoryId)
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
        [HttpPost("assign")]
        public async Task<IActionResult> AssignCase(CaseAssignDto caseAssignDto)
        {
            try
            {
                await _caseProcessingService.AssignTask(caseAssignDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("complete")]
        public async Task<IActionResult> CompleteCase(CaseCompleteDto caseCompeleteDto)
        {
            try
            {
                await _caseProcessingService.CompleteTask(caseCompeleteDto);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("revert")]
        public async Task<IActionResult> RevertCase(CaseRevertDto caseRevertDto)
        {
            try
            {
                await _caseProcessingService.RevertTask(caseRevertDto);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("transfer"), DisableRequestSizeLimit]
        public async Task<IActionResult> TransferCase()
        {
            try
            {
                //public Guid CaseHistoryId { get; set; }
                //public Guid ToEmployeeId { get; set; }
                //public Guid FromEmployeeId { get; set; }
                //public Guid CaseTypeId { get; set; }
                //public Guid ToStructureId { get; set; }
                //public string Remark { get; set; }

                CaseTransferDto caseTransferDto = new()
                {
                    CaseHistoryId = Guid.Parse(Request.Form["CaseHistoryId"]),
                    CaseTypeId = Guid.Parse(Request.Form["CaseTypeId"]),
                    FromEmployeeId = Guid.Parse(Request.Form["FromEmployeeId"]),
                    Remark = Request.Form["Remark"],
                    ToEmployeeId = Guid.Parse(Request.Form["ToEmployeeId"]),
                    ToStructureId = Guid.Parse(Request.Form["ToStructureId"])
                };

                await _caseProcessingService.TransferCase(caseTransferDto);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("waiting")]
        public async Task<IActionResult> AddToWaiting(Guid caseId)
        {
            try
            {
                await _caseProcessingService.AddToWaiting(caseId);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }




    }
}

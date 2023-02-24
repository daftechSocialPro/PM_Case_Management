using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Services.CaseMGMT.CaseAttachments;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseAttachementController : ControllerBase
    {
        private readonly ICaseAttachementService _caseAttachementService;

        public CaseAttachementController(ICaseAttachementService caseAttachementService)
        {
            _caseAttachementService = caseAttachementService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string CaseId = null)
        {
            try
            { 
                List<CaseAttachment> attachments = await _caseAttachementService.GetAttachements(CaseId);
                return Ok(attachments);
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

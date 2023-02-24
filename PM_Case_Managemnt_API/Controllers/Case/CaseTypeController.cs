using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Services.CaseService.CaseTypes;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/case")]
    [ApiController]
    public class CaseTypeController : ControllerBase
    {
        private readonly ICaseTypeService _caseTypeService;

        public CaseTypeController(ICaseTypeService caseTypeService)
        {
            _caseTypeService = caseTypeService;
        }

        [HttpGet("type")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CaseTypeGetDto> caseTypes =  await _caseTypeService.GetAllCaseTypes();
                return Ok(caseTypes);
            } catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("type")]
        public async Task<IActionResult> Create(CaseTypePostDto caseType)
        {
            try
            {
                await _caseTypeService.AddNewCaseType(caseType);
                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

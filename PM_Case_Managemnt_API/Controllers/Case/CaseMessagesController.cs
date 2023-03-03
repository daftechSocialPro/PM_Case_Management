using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.Services.CaseMGMT.CaseMessagesService;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/case")]
    [ApiController]
    public class CaseMessagesController : ControllerBase
    {
        private readonly ICaseMessagesService _caseMessagesService;

        public CaseMessagesController(ICaseMessagesService caseMessagesService)
        {
            _caseMessagesService = caseMessagesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMany(bool messageStatus)
        {
            try
            {
                return Ok(await _caseMessagesService.GetMany(messageStatus));
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

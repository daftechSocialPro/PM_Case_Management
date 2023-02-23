using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.Services.CaseService.Encode;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseEncodingController : ControllerBase
    {
        private readonly ICaseEncodeService _caseEncodeService;

        public CaseEncodingController(ICaseEncodeService caseEncodeService)
        {
                _caseEncodeService = caseEncodeService;
        }

        [HttpPost]
        public async Task Create()
        {

        }
    }
}

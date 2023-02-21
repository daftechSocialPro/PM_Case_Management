using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.DTOS.PM;
using PM_Case_Managemnt_API.Services.PM.Commite;

namespace PM_Case_Managemnt_API.Controllers.PM
{
    [Route("api/PM/[controller]")]
    [ApiController]
    public class CommiteController : Controller
    {
        private readonly ICommiteService _commiteService;
        public CommiteController(ICommiteService commiteService)
        {
            _commiteService = commiteService;
        }



        [HttpPost]
        public IActionResult Create([FromBody] AddCommiteDto addCommiteDto)
        {
            try
            {
                var response = _commiteService.AddCommite(addCommiteDto);
                return Ok(new { response });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error : {ex}");
            }
        }


        [HttpPut]
        public IActionResult Update([FromBody] UpdateCommiteDto updateCommiteDto)
        {
            try
            {
                var response = _commiteService.UpdateCommite(updateCommiteDto);
                return Ok(new { response });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error : {ex}");
            }
        }

        [HttpGet]
        public async Task<List<CommiteListDto>> GetCommiteLists()
        {
            var response = await _commiteService.GetCommiteLists();
            return response;
        }

        [HttpGet("getNotIncludedEmployees")]
        public async Task<List<SelectListDto>> GetNotIncludedEmployees(Guid commiteId)
        {
            var response = await _commiteService.GetNotIncludedEmployees(commiteId);

            return response;
        }
    }
}

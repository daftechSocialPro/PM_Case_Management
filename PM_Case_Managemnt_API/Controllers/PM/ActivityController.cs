using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.DTOS.PM;
using PM_Case_Managemnt_API.Services.PM.Activity;


namespace PM_Case_Managemnt_API.Controllers.PM
{
    [Route("api/PM/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {

        private readonly IActivityService _activityService;
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }


        [HttpPost]
        public IActionResult Create([FromBody] ActivityDetailDto addActivityDto)
        {
            try
            {
                var response = _activityService.AddActivityDetails(addActivityDto);
                return Ok(new { response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error : {ex}");
            }
        }

        [HttpPost("targetDivision")]
        public IActionResult AddTargetDivisionActivity (ActivityTargetDivisionDto activityTarget)
        {
            try
            {
                var response = _activityService.AddTargetActivities(activityTarget);
                return Ok(new { response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error : {ex}");
            }
        }
    }
}

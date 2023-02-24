using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Services.CaseService.FileSettings;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/case")]
    [ApiController]
    public class FileSettingController : ControllerBase
    {
        private readonly IFileSettingsService _fileSettingsService;

        public FileSettingController(IFileSettingsService fileSettingsService)
        {
            _fileSettingsService = fileSettingsService;
        }
        [HttpGet("fileSetting")]
        public async Task<IActionResult> GetAll()
        {
            try { 
                List<FileSettingGetDto> fileSettings = await _fileSettingsService.GetAllFileSettings();
                return Ok(fileSettings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("fileSetting")]
        public async Task<IActionResult> PostFileSetting(FileSettingPostDto fileSettingPostDto)
        {
            try
            {
                await _fileSettingsService.AddNewFileSetting(fileSettingPostDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Services.CaseMGMT.CaseAttachments;
using PM_Case_Managemnt_API.Services.CaseService.Encode;
using System.Net.Http.Headers;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/case")]
    [ApiController]
    public class CaseEncodingController : ControllerBase
    {
        private readonly ICaseEncodeService _caseEncodeService;
        private readonly ICaseAttachementService _caseAttachmentService;

        public CaseEncodingController(ICaseEncodeService caseEncodeService, ICaseAttachementService caseAttachementService)
        {
            _caseEncodeService = caseEncodeService;
            _caseAttachmentService = caseAttachementService;
        }


        [HttpPost("encoding"), DisableRequestSizeLimit]
        public async Task<IActionResult> Create()
        {
            try
            {



                CaseEncodePostDto caseEncodePostDto = new CaseEncodePostDto()
                {
                    CaseNumber = Request.Form["CaseNumber"],
                    LetterNumber = Request.Form["LetterNumber"],
                    LetterSubject = Request.Form["LetterSubject"],
                    CaseTypeId = Guid.Parse(Request.Form["CaseTypeId"]),
                    ApplicantId = Guid.Parse(Request.Form["ApplicantId"]),
                    EmployeeId = Guid.Parse(Request.Form["ApplicantId"]),
                    PhoneNumber2 = Request.Form["PhoneNumber2"],
                    Representative = Request.Form["Representative"],
                    CreatedBy = Guid.Parse(Request.Form["CreatedBy"]),


                };
                //string caseId = await _caseEncodeService.Add(caseEncodePostDto);

                if (Request.Form.Files.Any())
                {
                    List<CaseAttachment> attachments = new List<CaseAttachment>();
                    foreach (var file in Request.Form.Files)
                    {

                        if (file.Name.ToLower() == "attachemnts")
                        {
                            string folderName = Path.Combine("Assets", "CaseAttachments");
                            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                            //Create directory if not exists
                            (new FileInfo(folderName)).Directory.Create();

                            if (file.Length > 0)
                            {
                                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                string fullPath = Path.Combine(pathToSave, fileName);
                                string dbPath = Path.Combine(folderName, fileName);

                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    file.CopyTo(stream);
                                }
                                CaseAttachment attachment = new()
                                {
                                    Id = Guid.NewGuid(),
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = caseEncodePostDto.CreatedBy,
                                    RowStatus = RowStatus.Active,
                                    //CaseId = Guid.Parse(caseId),
                                    FilePath = dbPath
                                };
                                attachments.Add(attachment);
                            }

                        }
                        //else if (file.Name.ToString() == "filesettings")
                        //{
                        //    string folderName = Path.Combine("Assets", "FileSettings");
                        //    string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                        //    // Create the directory if not exists
                        //    (new FileInfo(folderName)).Directory.Create();

                        //    if (file.Length > 0)
                        //    {
                        //        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        //        string fullPath = Path.Combine(pathToSave, fileName);
                        //        string dbPath = Path.Combine(folderName, fileName);


                        //        using (var stream = new FileStream(fullPath, FileMode.Create))
                        //        {
                        //            file.CopyTo(stream);
                        //        }

                        //        FilesInformationPostDto filesInformation = new()
                        //        {
                        //            CreatedBy = caseEncodePostDto.CreatedBy,
                        //            CaseId = Guid.Parse(caseId),
                        //            FilePath = dbPath,
                        //        };
                        //    }
                        //}
                    }
                    await _caseAttachmentService.Add(attachments);
                }

                return NoContent();
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
                await _caseEncodeService.AssignTask(caseAssignDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("encoding")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _caseEncodeService.GetAll());

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet("getCaseNumber")]

        public async Task<string> getCaseNumebr()
        {

            return await _caseEncodeService.getCaseNumber();




        }

    }
}

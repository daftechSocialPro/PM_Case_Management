﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Services.CaseMGMT.CaseAttachments;
using PM_Case_Managemnt_API.Services.CaseMGMT.FileInformationService;
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
        private readonly IFilesInformationService _filesInformationService;

        public CaseEncodingController(ICaseEncodeService caseEncodeService, ICaseAttachementService caseAttachementService, IFilesInformationService filesInformationService)
        {
            _caseEncodeService = caseEncodeService;
            _caseAttachmentService = caseAttachementService;
            _filesInformationService = filesInformationService;
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
                string caseId = await _caseEncodeService.Add(caseEncodePostDto);

                if (Request.Form.Files.Any())
                {
                    List<CaseAttachment> attachments = new List<CaseAttachment>();
                    List<FilesInformation> fileInfos = new List<FilesInformation>();
                    foreach (var file in Request.Form.Files)
                    {

                        if (file.Name.ToLower() == "attachments")
                        {
                            string folderName = Path.Combine("Assets", "CaseAttachments");
                            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                            //Create directory if not exists
                            if (!Directory.Exists(pathToSave))
                                Directory.CreateDirectory(pathToSave);

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
                                    CaseId = Guid.Parse(caseId),
                                    FilePath = dbPath
                                };
                                attachments.Add(attachment);
                            }

                        }
                        else if (file.Name.ToLower() == "filesettings")
                        {
                            string folderName = Path.Combine("Assets", "FileSettings");
                            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                            // Create the directory if not exists
                            if (!Directory.Exists(pathToSave))
                                Directory.CreateDirectory(pathToSave);

                            if (file.Length > 0)
                            {
                                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                string fullPath = Path.Combine(pathToSave, fileName);
                                string dbPath = Path.Combine(folderName, fileName);


                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    file.CopyTo(stream);
                                }

                                FilesInformation filesInformation = new()
                                {
                                    Id = Guid.NewGuid(),
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = caseEncodePostDto.CreatedBy,
                                    RowStatus = RowStatus.Active,
                                    FilePath = dbPath,
                                    FileSettingId = Guid.Parse(fileName.Split(".")[0]),
                                    CaseId = Guid.Parse(caseId),
                                    filetype = file.ContentType
                                };
                                fileInfos.Add(filesInformation);

                            }
                        }
                    }
                    await _caseAttachmentService.AddMany(attachments);
                    await _filesInformationService.AddMany(fileInfos);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }
   

        [HttpGet("encoding")]
        public async Task<IActionResult> GetAll(Guid userId)
        {
            try
            {
                return Ok(await _caseEncodeService.GetAll(userId));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet("getCaseNumber")]

        public async Task<string> GetCaseNumebr()
        {

            return await _caseEncodeService.GetCaseNumber();




        }
        [HttpGet("getnotification")]
        public async Task<IActionResult> GetNotification(Guid employeeId)
        {
            try
            {
                return Ok(await _caseEncodeService.GetAllTransfred(employeeId));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }
        [HttpGet("mycaseList")]
        public async Task<IActionResult> MyCaseList(Guid employeeId)
        {
            try
            {
                return Ok(await _caseEncodeService.MyCaseList(employeeId));

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }
        [HttpGet("completedList")]
        public async Task<IActionResult> CompletedCases()
        {
            try
            {
                return Ok(await _caseEncodeService.CompletedCases());

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }
        [HttpGet("getArchivedCases")]

        public async Task<IActionResult> GetArchivedCases()
        {
            try
            {
                return Ok(await _caseEncodeService.GetArchivedCases());

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }






    }
}

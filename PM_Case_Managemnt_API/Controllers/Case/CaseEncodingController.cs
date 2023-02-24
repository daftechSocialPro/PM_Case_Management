﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Services.CaseMGMT.CaseAttachments;
using PM_Case_Managemnt_API.Services.CaseService.Encode;
using System.Net.Http.Headers;

namespace PM_Case_Managemnt_API.Controllers.Case
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CaseEncodePostDto caseEncodePostDto)
        {
            try
            {
                string caseId = await _caseEncodeService.AddCaseEncoding(caseEncodePostDto);

                if (Request.Form.Files.Any())
                {
                    List<CaseAttachment> attachments = new List<CaseAttachment>();
                    foreach (var file in Request.Form.Files)
                    {
                        string folderName = Path.Combine("Assets", "CaseAttachments");
                        string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


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
                    await _caseAttachmentService.AddAttachemnts(attachments);
                }

                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CaseEncodeGetDto> cases = await _caseEncodeService.GetCaseEncodings();
                return Ok(cases);

            } catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

    }
}

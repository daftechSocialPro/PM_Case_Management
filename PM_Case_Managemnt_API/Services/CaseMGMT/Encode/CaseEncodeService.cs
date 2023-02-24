using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using System.Runtime.CompilerServices;

namespace PM_Case_Managemnt_API.Services.CaseService.Encode
{
    public class CaseEncodeService: ICaseEncodeService
    {
        private readonly DBContext _dbContext;

        public CaseEncodeService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> AddCaseEncoding(CaseEncodePostDto caseEncodePostDto)
        {
            try
            {
                if (caseEncodePostDto.EmployeeId == null && caseEncodePostDto.ApplicantId == null)
                    throw new Exception("Please Provide an Applicant ID or Employee ID");

                Case newCase = new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    RowStatus = Models.Common.RowStatus.Active,
                    CreatedBy = caseEncodePostDto.CreatedBy,
                    CaseNumber = caseEncodePostDto.CaseNumber,
                    ApplicantId = caseEncodePostDto.ApplicantId,
                    EmployeeId = caseEncodePostDto.EmployeeId,
                    LetterNumber = caseEncodePostDto.LetterNumber,
                    LetterSubject = caseEncodePostDto.LetterSubject,
                    CaseTypeId = caseEncodePostDto.CaseTypeId,
                    AffairStatus = caseEncodePostDto.AffairStatus,
                    PhoneNumber2 = caseEncodePostDto.PhoneNumber2,
                    Representative = caseEncodePostDto.Representative,
                    Remark = caseEncodePostDto.Remark   
                };

                await _dbContext.AddAsync(newCase);
                await _dbContext.SaveChangesAsync();

                return newCase.Id.ToString();
            } catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CaseEncodeGetDto>> GetCaseEncodings()
        {
            try
            {
                List<Case> cases = _dbContext.Cases.Include(p => p.Employee).Include(p => p.CaseType).Include(p => p.Applicant).ToList();
                List<CaseEncodeGetDto> results = new List<CaseEncodeGetDto>();

                foreach (Case currCase in cases)
                {
                    results.Add(new CaseEncodeGetDto
                    {
                        Id = currCase.Id,
                        CreatedAt = currCase.CreatedAt,
                        CreatedBy = currCase.CreatedBy,
                        AffairStatus = currCase.AffairStatus,
                        PhoneNumber2 = currCase.PhoneNumber2,
                        ApplicantId = currCase.ApplicantId,
                        CaseNumber = currCase.CaseNumber,
                        CaseTypeId = currCase.CaseTypeId,
                        EmployeeId = currCase.EmployeeId,
                        IsArchived = currCase.IsArchived,
                        LetterNumber = currCase.LetterNumber,
                        LetterSubject = currCase.LetterSubject,
                        Remark = currCase.Remark,
                        Representative = currCase.Representative,
                        SMSStatus = currCase.SMSStatus,
                        CaseType = currCase.CaseType,
                        Employee = currCase.Employee,
                        Applicant = currCase.Applicant,
                    });
                }

                return results;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

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

        public async Task AddCaseEncoding(CaseEncodePostDto caseEncodePostDto)
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
            } catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }


    }
}

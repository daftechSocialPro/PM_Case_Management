using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Services.CaseMGMT.History;
using System.Runtime.CompilerServices;

namespace PM_Case_Managemnt_API.Services.CaseService.Encode
{
    public class CaseEncodeService: ICaseEncodeService
    {
        private readonly DBContext _dbContext;
        private readonly ICaseHistoryService _caseHistoryService;
        public CaseEncodeService(DBContext dbContext, ICaseHistoryService caseHistoryService)
        {
            _dbContext = dbContext;
            _caseHistoryService = caseHistoryService;
        }

        public async Task<string> Add(CaseEncodePostDto caseEncodePostDto)
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
                    ApplicantId = caseEncodePostDto.ApplicantId,
                    //EmployeeId = caseEncodePostDto.EmployeeId,
                    LetterNumber = caseEncodePostDto.LetterNumber,
                    LetterSubject = caseEncodePostDto.LetterSubject,
                    CaseTypeId = caseEncodePostDto.CaseTypeId,
                    AffairStatus = AffairStatus.Encoded,
                    PhoneNumber2 = caseEncodePostDto.PhoneNumber2,
                    Representative = caseEncodePostDto.Representative,
                 
                };
                string caseNumber = await getCaseNumber();
                newCase.CaseNumber = caseNumber;

                await _dbContext.AddAsync(newCase);
                await _dbContext.SaveChangesAsync();

                CaseHistoryPostDto history = new()
                {
                    CreatedBy = caseEncodePostDto.CreatedBy,
                    CaseId = newCase.Id,
                    CaseTypeId = newCase.CaseTypeId,
                    ToEmployeeId = null,
                    FromStructureId = null,
                    ToStructureId = null,
                    AffairHistoryStatus = AffairHistoryStatus.Waiting,
                };


                await _caseHistoryService.Add(history);

                return newCase.Id.ToString();
            } catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CaseEncodeGetDto>> GetAll(Guid userId)
        {
            try
            {
                List<CaseEncodeGetDto> cases = await _dbContext.Cases.Where(ca => ca.CreatedBy.Equals(userId) && ca.AffairStatus.Equals(AffairStatus.Encoded)).Include(p => p.Employee).Include(p => p.CaseType).Include(p => p.Applicant).Select(st => new CaseEncodeGetDto
                {
                    Id = st.Id,
                    CaseNumber = st.CaseNumber,
                    LetterNumber = st.LetterNumber,
                    LetterSubject = st.LetterSubject,
                    CaseTypeName = st.CaseType.CaseTypeTitle,
                    ApplicantName = st.Applicant.ApplicantName,
                    EmployeeName = st.Employee.FullName,
                    ApplicantPhoneNo = st.Applicant.PhoneNumber,
                    EmployeePhoneNo = st.Employee.PhoneNumber,
                    CreatedAt = st.CreatedAt.ToString(),
                }).ToListAsync();

                return cases;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AssignTask(CaseAssignDto caseAssignDto)
        {
            try
            {
                //Case caseToAssign = await _dbContext.Cases.SingleOrDefaultAsync(el => el.Id.Equals())
                CaseHistory caseHistory = await _dbContext.CaseHistories.SingleOrDefaultAsync(el => el.CaseId.Equals(caseAssignDto.CaseId));

                if (caseHistory == null)
                    throw new Exception("No Case found for the given ID.");

                caseHistory.ToStructureId = caseAssignDto.ForwardedToStructureId;
                caseHistory.ToEmployeeId = caseAssignDto.ForwardedToEmployeeId;
                caseHistory.ForwardedById = caseAssignDto.ForwardedByEmployeeId;
                

                _dbContext.Entry(caseHistory).State = EntityState.Modified;

                Case currCase = await _dbContext.Cases.SingleOrDefaultAsync(el => el.Id.Equals(caseAssignDto.CaseId));
                currCase.AffairStatus = AffairStatus.Assigned;

                _dbContext.Entry(currCase).State = EntityState.Modified;
                
                await _dbContext.SaveChangesAsync();

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<string> getCaseNumber()
        {
            string CaseNumber = "DDC2015-";

            var latestNumber =  _dbContext.Cases.OrderByDescending(x=>x.CreatedBy).Select(c => c.CaseNumber).FirstOrDefault();

            if (latestNumber!=null)
            {
                int currCaseNumber = int.Parse(latestNumber.Split('-')[1]);
                CaseNumber += (currCaseNumber + 1).ToString(); 
            }else
            {
                CaseNumber += "1";
            }

            return CaseNumber;

        }


    }
}

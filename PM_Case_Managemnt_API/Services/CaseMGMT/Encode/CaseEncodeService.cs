using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Services.CaseMGMT.CaseForwardService;
using PM_Case_Managemnt_API.Services.CaseMGMT.History;
using System.Runtime.CompilerServices;

namespace PM_Case_Managemnt_API.Services.CaseService.Encode
{
    public class CaseEncodeService: ICaseEncodeService
    {
        private readonly DBContext _dbContext;
        private readonly ICaseHistoryService _caseHistoryService;
        private readonly ICaseForwardService _caseForwardService;
        public CaseEncodeService(DBContext dbContext, ICaseHistoryService caseHistoryService, ICaseForwardService caseForwardService)
        {
            _dbContext = dbContext;
            _caseHistoryService = caseHistoryService;
            _caseForwardService = caseForwardService;
        }

        public async Task<string> Add(CaseEncodePostDto caseEncodePostDto)
        {
            try
            {
                if (caseEncodePostDto.EmployeeId == null && caseEncodePostDto.ApplicantId == null)
                    throw new Exception("Please Provide an Applicant ID or Employee ID");


                // structure of current structure.
                var curStructc = from e in _dbContext.Employees
                                 where e.Id.Equals(caseEncodePostDto.EncoderEmpId)
                                   join es in _dbContext.EmployeesStructures on e.Id equals es.EmployeeId
                                   select es.OrganizationalStructureId;

                Guid structureId = (Guid)curStructc.First();

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
                    FromStructureId = structureId,
                    ToEmployeeId = null,
                    ToStructureId = null,
                    AffairHistoryStatus = AffairHistoryStatus.Pend,
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

                caseHistory.ToStructureId = caseAssignDto.AssignedToStructureId;
                caseHistory.ToEmployeeId = caseAssignDto.AssignedToEmployeeId;
                caseHistory.ForwardedById = caseAssignDto.AssignedByEmployeeId;


                _dbContext.Entry(caseHistory).Property(caseHistory => caseHistory.ToStructureId).IsModified = true;
                _dbContext.Entry(caseHistory).Property(caseHistory => caseHistory.ToEmployeeId).IsModified = true;
                _dbContext.Entry(caseHistory).Property(caseHistory => caseHistory.ForwardedById).IsModified = true;


                Case currCase = await _dbContext.Cases.SingleOrDefaultAsync(el => el.Id.Equals(caseAssignDto.CaseId));
                currCase.AffairStatus = AffairStatus.Assigned;

                _dbContext.Entry(currCase).Property(curr => curr.AffairStatus).IsModified = true;
                await _dbContext.SaveChangesAsync();
                if (caseAssignDto.ForwardedToStructureId != null)
                {
                    CaseForwardPostDto caseFwd = new()
                    {
                        CaseId = caseAssignDto.CaseId,
                        CreatedBy = currCase.CreatedBy,
                        ForwardedByEmployeeId = caseAssignDto.AssignedByEmployeeId,
                        ForwardedToStructureId = caseAssignDto.ForwardedToStructureId
                    };

                    await _caseForwardService.AddMany(caseFwd);
                }



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

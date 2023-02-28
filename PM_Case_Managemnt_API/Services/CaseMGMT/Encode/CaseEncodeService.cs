using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Services.CaseMGMT.CaseForwardService;
using PM_Case_Managemnt_API.Services.CaseMGMT.History;
using System.Runtime.CompilerServices;

namespace PM_Case_Managemnt_API.Services.CaseService.Encode
{
    public class CaseEncodeService : ICaseEncodeService
    {
        private readonly DBContext _dbContext;
        private readonly AuthenticationContext _authContext;
        private readonly ICaseHistoryService _caseHistoryService;
        private readonly ICaseForwardService _caseForwardService;
        public CaseEncodeService(DBContext dbContext, AuthenticationContext authContext, ICaseHistoryService caseHistoryService, ICaseForwardService caseForwardService)
        {
            _dbContext = dbContext;
            _authContext = authContext;
            _caseHistoryService = caseHistoryService;
            _caseForwardService = caseForwardService;

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



                return newCase.Id.ToString();
            }
            catch (Exception ex)
            {
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
                string userId = _authContext.ApplicationUsers.Where(x => x.EmployeesId == caseAssignDto.AssignedByEmployeeId).FirstOrDefault().Id;
                Case caseToAssign = await _dbContext.Cases.SingleOrDefaultAsync(el => el.Id.Equals(caseAssignDto.CaseId));
                // CaseHistory caseHistory = await _dbContext.CaseHistories.SingleOrDefaultAsync(el => el.CaseId.Equals(caseAssignDto.CaseId));

                var toEmployee = caseAssignDto.AssignedToEmployeeId == Guid.Empty || caseAssignDto.AssignedToEmployeeId == null ?
             _dbContext.Employees.FirstOrDefault(
                 e =>
                     e.OrganizationalStructureId == caseAssignDto.AssignedToStructureId &&
                     e.Position == Position.Director).Id : caseAssignDto.AssignedToEmployeeId;


                var fromEmployeestructure = _dbContext.Employees.Include(x => x.OrganizationalStructure).Where(x => x.Id == caseAssignDto.AssignedByEmployeeId).First().OrganizationalStructure.Id;

                var startupHistory = new CaseHistory
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.Parse(userId),
                    RowStatus = RowStatus.Active,
                    CaseId = caseAssignDto.CaseId,
                    CaseTypeId = _dbContext.Cases.Find(caseAssignDto.CaseId).CaseTypeId,
                    AffairHistoryStatus = AffairHistoryStatus.Pend,
                    FromEmployeeId = caseAssignDto.AssignedByEmployeeId,
                    FromStructureId = fromEmployeestructure,
                    ReciverType = ReciverType.Orginal,
                    ToStructureId = caseAssignDto.AssignedToStructureId,
                    ToEmployeeId = toEmployee,

                };
                startupHistory.SecreateryNeeded = (caseAssignDto.AssignedToEmployeeId == Guid.Empty || caseAssignDto.AssignedToEmployeeId == null) ? true : false;

                caseToAssign.AffairStatus = AffairStatus.Assigned;
                _dbContext.Entry(caseToAssign).Property(x => x.AffairStatus).IsModified = true;

                _dbContext.CaseHistories.Add(startupHistory);
                _dbContext.SaveChanges();






                foreach (var row in caseAssignDto.ForwardedToStructureId)
                {


                    if (toEmployee != null)
                    {
                        toEmployee =
                         _dbContext.Employees.FirstOrDefault(
                             e =>
                                 e.OrganizationalStructureId == row &&
                                 e.Position == Position.Director) == null ? null : _dbContext.Employees.FirstOrDefault(
                             e =>
                                 e.OrganizationalStructureId == row &&
                                 e.Position == Position.Director).Id;

                        CaseHistory history = new()
                        {
                            Id = Guid.NewGuid(),
                            CreatedAt = DateTime.Now,
                            CreatedBy = Guid.Parse(userId),
                            CaseId = caseAssignDto.CaseId,
                            AffairHistoryStatus = AffairHistoryStatus.Pend,
                            FromEmployeeId = caseAssignDto.AssignedByEmployeeId,
                            FromStructureId = fromEmployeestructure,
                            ToStructureId = row,
                            ReciverType = ReciverType.Cc,
                            ToEmployeeId = toEmployee,
                            RowStatus = RowStatus.Active,

                        };


                        _dbContext.CaseHistories.Add(history);
                        _dbContext.SaveChanges();
                    }



                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<string> getCaseNumber()
        {
            string CaseNumber = "DDC2015-";

            var latestNumber = _dbContext.Cases.OrderByDescending(x => x.CreatedBy).Select(c => c.CaseNumber).FirstOrDefault();

            if (latestNumber != null)
            {
                int currCaseNumber = int.Parse(latestNumber.Split('-')[1]);
                CaseNumber += (currCaseNumber + 1).ToString();
            }
            else
            {
                CaseNumber += "1";
            }

            return CaseNumber;

        }
        public async Task<List<CaseEncodeGetDto>> GetAllTransfred(Guid employeeId)


        {

            Employee user = _dbContext.Employees.Include(x => x.OrganizationalStructure).Where(x => x.Id == employeeId).FirstOrDefault();

            List<CaseEncodeGetDto> notfications = new List<CaseEncodeGetDto>();





            if (user.Position == Position.Secertary)
            {
                notfications =await  _dbContext.CaseHistories.Include(c
                   => c.Case.CaseType).Include(x => x.Case.Applicant).Where(x => x.ToStructureId == user.OrganizationalStructureId &&
                 (x.AffairHistoryStatus == AffairHistoryStatus.Pend || x.AffairHistoryStatus == AffairHistoryStatus.Transfered) &&
                 (!x.IsConfirmedBySeretery || !x.IsForwardedBySeretery)).Select(x => new CaseEncodeGetDto
                 {
                     Id = x.Id,
                     CaseTypeName = x.Case.CaseType.CaseTypeTitle,
                     CaseNumber = x.Case.CaseNumber,
                     CreatedAt = x.Case.CreatedAt.ToString(),
                     ApplicantName = x.Case.Applicant.ApplicantName,
                     ApplicantPhoneNo = x.Case.Applicant.PhoneNumber,
                     EmployeeName = x.Case.Employee.FullName,
                     EmployeePhoneNo = x.Case.Employee.PhoneNumber,
                     LetterNumber = x.Case.LetterNumber,
                     LetterSubject = x.Case.LetterSubject

                 }).ToListAsync();


            }
            else
            {

                notfications = await _dbContext.CaseHistories.Where(x => x.ToEmployeeId == employeeId && (x.AffairHistoryStatus == AffairHistoryStatus.Pend || x.AffairHistoryStatus == AffairHistoryStatus.Transfered)).Select(x => new CaseEncodeGetDto
                {
                    Id = x.Id,
                    CaseTypeName = x.Case.CaseType.CaseTypeTitle,
                    CaseNumber = x.Case.CaseNumber,
                    CreatedAt = x.Case.CreatedAt.ToString(),
                    ApplicantName = x.Case.Applicant.ApplicantName,
                    ApplicantPhoneNo = x.Case.Applicant.PhoneNumber,
                    EmployeeName = x.Case.Employee.FullName, 
                    EmployeePhoneNo = x.Case.Employee.PhoneNumber,
                    LetterNumber = x.Case.LetterNumber,
                    LetterSubject = x.Case.LetterSubject

                }).ToListAsync(); ;
            }


            return notfications;

        }


    }
}

using Microsoft.AspNetCore.Hosting.Server;
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
        private readonly ICaseHistoryService _caseHistoryService;
        private readonly ICaseForwardService _caseForwardService;
        private readonly AuthenticationContext _authenticationContext;
        public CaseEncodeService(DBContext dbContext, AuthenticationContext authenticationContext, ICaseHistoryService caseHistoryService, ICaseForwardService caseForwardService)
        {
            _dbContext = dbContext;
            _authenticationContext = authenticationContext;
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
                string userId = _authenticationContext.ApplicationUsers.Where(x => x.EmployeesId == caseAssignDto.AssignedByEmployeeId).FirstOrDefault().Id;
                Case caseToAssign = await _dbContext.Cases.SingleOrDefaultAsync(el => el.Id.Equals(caseAssignDto.CaseId));
                // CaseHistory caseHistory = await _dbContext.CaseHistories.SingleOrDefaultAsync(el => el.CaseId.Equals(caseAssignDto.CaseId));

                var toEmployee = caseAssignDto.AssignedToEmployeeId == Guid.Empty || caseAssignDto.AssignedToEmployeeId == null ?
             _dbContext.Employees.FirstOrDefault(
                 e =>
                     e.OrganizationalStructureId == caseAssignDto.AssignedToStructureId &&
                     e.Position == Position.Director).Id : caseAssignDto.AssignedToEmployeeId;


                var fromEmployeestructure = _dbContext.Employees.Include(x => x.OrganizationalStructure).Where(x => x.Id == caseAssignDto.AssignedByEmployeeId).First().OrganizationalStructure.Id;


                Case currCase = await _dbContext.Cases.SingleOrDefaultAsync(el => el.Id.Equals(caseAssignDto.CaseId));
                currCase.AffairStatus = AffairStatus.Assigned;

                _dbContext.Entry(currCase).Property(curr => curr.AffairStatus).IsModified = true;
                await _dbContext.SaveChangesAsync();
                if (caseAssignDto.ForwardedToStructureId != null)
                {
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CompleteTask(CaseCompleteDto caseCompleteDto)
        {
            try
            {
                CaseHistory selectedHistory = _dbContext.CaseHistories.Find(caseCompleteDto.CaseHistoryId);
                selectedHistory.AffairHistoryStatus = AffairHistoryStatus.Completed;
                selectedHistory.CompletedDateTime = DateTime.Now;
                selectedHistory.Remark = caseCompleteDto.Remark;

                Case currentCase = await _dbContext.Cases.Include(x => x.Applicant).Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == selectedHistory.CaseId);
                CaseHistory currentHist = await _dbContext.CaseHistories.Include(x => x.Case).Include(x => x.ToStructure).FirstOrDefaultAsync(x => x.Id == selectedHistory.Id);

                if (currentCase != null)
                {
                    if (selectedHistory != null)
                    {

                    }
                }

                _dbContext.CaseHistories.Attach(selectedHistory);
                _dbContext.Entry(selectedHistory).Property(x => x.AffairHistoryStatus).IsModified = true;
                _dbContext.Entry(selectedHistory).Property(x => x.CompletedDateTime).IsModified = true;
                _dbContext.Entry(selectedHistory).Property(x => x.Remark).IsModified = true;
                //_dbContext.Entry(selectedHistory).Property(x => x.IsSmsSent).IsModified = true;
                //_dbContext.SaveChanges();

                var selectedCase = _dbContext.Cases.Find(selectedHistory.CaseId);
                selectedCase.CompletedAt = DateTime.Now;
                selectedCase.AffairStatus = AffairStatus.Completed;


                _dbContext.Cases.Attach(selectedCase);
                _dbContext.Entry(selectedCase).Property(x => x.CompletedAt).IsModified = true;
                _dbContext.Entry(selectedCase).Property(x => x.AffairStatus).IsModified = true;


                await _dbContext.SaveChangesAsync();



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task RevertTask(CaseRevertDto revertCase)
        {
            try
            {

                Employee currEmp = await _dbContext.Employees.Include(x => x.OrganizationalStructure).Where(x => x.Id.Equals(revertCase.EmployeeId)).FirstOrDefaultAsync();
                CaseHistory selectedHistory = _dbContext.CaseHistories.Find(revertCase.CaseHistoryId);

                selectedHistory.AffairHistoryStatus = AffairHistoryStatus.Revert;
                selectedHistory.RevertedAt = DateTime.Now;
                selectedHistory.Remark = revertCase.Remark;
                _dbContext.CaseHistories.Attach(selectedHistory);
                _dbContext.Entry(selectedHistory).Property(x => x.AffairHistoryStatus).IsModified = true;
                _dbContext.Entry(selectedHistory).Property(x => x.RevertedAt).IsModified = true;
                _dbContext.Entry(selectedHistory).Property(x => x.Remark).IsModified = true;
                CaseHistory newHistory = new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.Parse(_authenticationContext.ApplicationUsers.Where(appUsr => appUsr.EmployeesId.Equals(revertCase.EmployeeId)).First().Id),
                    RowStatus = RowStatus.Active,
                    FromEmployeeId = revertCase.EmployeeId,
                    FromStructureId = currEmp.OrganizationalStructureId,
                    ToEmployeeId = selectedHistory.FromEmployeeId,
                    ToStructureId = selectedHistory.FromStructureId,
                    Remark = "",
                    CaseId = selectedHistory.CaseId,
                    ReciverType = ReciverType.Orginal,
                };
                _dbContext.CaseHistories.Add(newHistory);


                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task TransferCase(CaseTransferDto caseTransferDto)
        {
            try
            {
                //var currentUser = DbContext.Users.Find(User.Identity.GetUserId());
                Employee currEmp = await _dbContext.Employees.Where(el => el.Id.Equals(caseTransferDto.FromEmployeeId)).FirstOrDefaultAsync();
                CaseHistory currentLastHistory = await _dbContext.CaseHistories.Where(el => el.Id.Equals(caseTransferDto.CaseHistoryId)).OrderByDescending(x => x.CreatedAt).FirstAsync();
                currentLastHistory.AffairHistoryStatus = AffairHistoryStatus.Transfered;
                currentLastHistory.TransferedDateTime = DateTime.Now;
                _dbContext.CaseHistories.Attach(currentLastHistory);
                _dbContext.Entry(currentLastHistory).Property(c => c.AffairHistoryStatus).IsModified = true;
                _dbContext.Entry(currentLastHistory).Property(c => c.TransferedDateTime).IsModified = true;

                var newHistory = new CaseHistory
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.Parse(_authenticationContext.ApplicationUsers.Where(appUsr => appUsr.EmployeesId.Equals(caseTransferDto.ToEmployeeId)).First().Id),
                    RowStatus = RowStatus.Active,
                    FromEmployeeId = caseTransferDto.FromEmployeeId,
                    FromStructureId = currEmp.OrganizationalStructureId,
                    ToEmployeeId = caseTransferDto.FromEmployeeId,
                    ToStructureId = caseTransferDto.FromEmployeeId,
                    Remark = caseTransferDto.Remark,
                    CaseId = currentLastHistory.CaseId,
                    ReciverType = ReciverType.Orginal,
                    CaseTypeId = caseTransferDto.CaseTypeId
                };


                await _dbContext.CaseHistories.AddAsync(newHistory);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)

            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddToWaiting(Guid caseId)
        {
            try
            {
                CaseHistory currHistory = await _dbContext.CaseHistories.Where(el => el.CaseId.Equals(caseId)).FirstAsync();

                currHistory.AffairHistoryStatus = AffairHistoryStatus.Waiting;

                await _dbContext.SaveChangesAsync();

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
                notfications = await _dbContext.CaseHistories.Include(c
                   => c.Case.CaseType).Include(x => x.Case.Applicant).Where(x => x.ToStructureId == user.OrganizationalStructureId &&
                 (x.AffairHistoryStatus == AffairHistoryStatus.Pend || x.AffairHistoryStatus == AffairHistoryStatus.Transfered) &&
                 (!x.IsConfirmedBySeretery )).Select(x => new CaseEncodeGetDto
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
                     LetterSubject = x.Case.LetterSubject,
                     FromStructure = x.FromStructure.StructureName,
                     ToEmployee = x.ToEmployee.FullName,
                     ToStructure = x.ToStructure.StructureName,
                     FromEmployeeId = x.FromEmployee.FullName,
                     ReciverType = x.ReciverType.ToString(),
                     SecreateryNeeded = x.SecreateryNeeded,
                     IsConfirmedBySeretery = x.IsConfirmedBySeretery,
                     Position = user.Position.ToString(),
                     AffairHistoryStatus = x.AffairHistoryStatus.ToString()

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
                    LetterSubject = x.Case.LetterSubject,
                    FromStructure = x.FromStructure.StructureName,
                    FromEmployeeId = x.FromEmployee.FullName,
                    ReciverType = x.ReciverType.ToString(),
                    SecreateryNeeded = x.SecreateryNeeded,
                    IsConfirmedBySeretery = x.IsConfirmedBySeretery,
                    AffairHistoryStatus = x.AffairHistoryStatus.ToString(),
                    ToEmployee = x.ToEmployee.FullName,
                    ToStructure = x.ToStructure.StructureName,
                    Position = user.Position.ToString(),

                }).ToListAsync(); ;
            }


            return notfications;

        }



        public async Task<List<CaseEncodeGetDto>> MyCaseList(Guid employeeId)
        {
            Employee user = _dbContext.Employees.Include(x => x.OrganizationalStructure).Where(x => x.Id == employeeId).FirstOrDefault();
            

            if (user.Position == Position.Secertary)
            {
                var HeadEmployees =
                    _dbContext.Employees.Include(x => x.OrganizationalStructure).Where(
                        x =>
                            x.OrganizationalStructureId == user.OrganizationalStructureId &&
                            x.Position == Position.Director).ToList();
                var allAffairHistory =await  _dbContext.CaseHistories
                    .Include(x => x.Case)
                    .Include(x => x.FromEmployee)
                    .Include(x => x.FromStructure)
                    .OrderByDescending(x => x.CreatedAt)
                    .Where(x => ((x.ToEmployee.OrganizationalStructureId == user.OrganizationalStructureId &&
                                x.ToEmployee.Position == Position.Director && !x.IsConfirmedBySeretery)
                                || (x.FromEmployee.OrganizationalStructureId == user.OrganizationalStructureId &&
                                x.FromEmployee.Position == Position.Director &&
                                !x.IsForwardedBySeretery &&
                                !x.IsConfirmedBySeretery && x.SecreateryNeeded)
                                ) && x.AffairHistoryStatus != AffairHistoryStatus.Seen).Select(x => new CaseEncodeGetDto
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
                                    LetterSubject = x.Case.LetterSubject,
                                    Position = user.Position.ToString(),
                                    FromStructure = x.FromStructure.StructureName,
                                    FromEmployeeId = x.FromEmployee.FullName,
                                    ReciverType = x.ReciverType.ToString(),
                                    SecreateryNeeded= x.SecreateryNeeded,
                                    IsConfirmedBySeretery = x.IsConfirmedBySeretery,
                                    ToEmployee = x.ToEmployee.FullName,
                                    ToStructure = x.ToStructure.StructureName,
                                    AffairHistoryStatus = x.AffairHistoryStatus.ToString()
                                }).ToListAsync();
                ;

      
        
                return allAffairHistory;
            }
            else
            {
                var allAffairHistory =await  _dbContext.CaseHistories
                .Include(x => x.Case)

                .Include(x => x.FromEmployee)
                .Include(x => x.FromStructure)
                .OrderByDescending(x => x.CreatedAt)
                .Where(x => x.AffairHistoryStatus != AffairHistoryStatus.Completed
                            && x.AffairHistoryStatus != AffairHistoryStatus.Waiting
                            && x.AffairHistoryStatus != AffairHistoryStatus.Transfered
                            && x.AffairHistoryStatus != AffairHistoryStatus.Revert
                            && x.ToEmployeeId == employeeId).Select(x => new CaseEncodeGetDto
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
                                LetterSubject = x.Case.LetterSubject,
                                Position = user.Position.ToString(),
                                FromStructure = x.FromStructure.StructureName,
                                FromEmployeeId = x.FromEmployee.FullName,
                                ReciverType = x.ReciverType.ToString(),
                                SecreateryNeeded = x.SecreateryNeeded,
                                IsConfirmedBySeretery = x.IsConfirmedBySeretery,
                                ToEmployee = x.ToEmployee.FullName,
                                ToStructure = x.ToStructure.StructureName,
                                AffairHistoryStatus = x.AffairHistoryStatus.ToString()
                            }).ToListAsync();

                return allAffairHistory;
            }

        }

     
    }



}





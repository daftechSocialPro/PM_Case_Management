using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Services.CaseMGMT.CaseForwardService;
using PM_Case_Managemnt_API.Services.CaseMGMT.History;

namespace PM_Case_Managemnt_API.Services.CaseMGMT
{
    public class CaseProccessingService : ICaseProccessingService
    {

        private readonly DBContext _dbContext;

        public CaseProccessingService(DBContext dbContext)
        {
            _dbContext = dbContext;


        }

        public async Task<int> ConfirmTranasaction(ConfirmTranscationDto confirmTranscationDto)
        {

            try
            {

                var history = _dbContext.CaseHistories.Find(confirmTranscationDto.CaseHistoryId);
                history.IsConfirmedBySeretery = true;
                history.SecreteryConfirmationDateTime = DateTime.Now;
                history.SecreteryId = confirmTranscationDto.EmployeeId;

                _dbContext.CaseHistories.Attach(history);
                _dbContext.Entry(history).Property(c => c.IsConfirmedBySeretery).IsModified = true;
                _dbContext.Entry(history).Property(c => c.SecreteryConfirmationDateTime).IsModified = true;
                _dbContext.Entry(history).Property(c => c.SecreteryId).IsModified = true;
                _dbContext.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CaseEncodeGetDto> GetCaseDetial(Guid historyId, Guid employeeId)
        {

            Employee user = _dbContext.Employees.Include(x => x.OrganizationalStructure).Where(x => x.Id == employeeId).FirstOrDefault();


            CaseHistory currentHistry = _dbContext.CaseHistories
                .Include(x => x.Case)
                .Include(x => x.FromEmployee)
                .Include(x => x.FromStructure)
                .Include(x => x.Case)?.FirstOrDefault(x => x.AffairHistoryStatus != AffairHistoryStatus.Completed
                                     && x.ToEmployeeId == employeeId
                                     && x.Id == historyId);




            if (currentHistry != null && (currentHistry.AffairHistoryStatus == AffairHistoryStatus.Pend || currentHistry.AffairHistoryStatus == AffairHistoryStatus.Waiting))
            {
                currentHistry.AffairHistoryStatus = AffairHistoryStatus.Seen;
                currentHistry.SeenDateTime = DateTime.Now;
                _dbContext.CaseHistories.Attach(currentHistry);
                _dbContext.Entry(currentHistry).Property(x => x.AffairHistoryStatus).IsModified = true;
                _dbContext.Entry(currentHistry).Property(x => x.SeenDateTime).IsModified = true;
                _dbContext.SaveChanges();
            }



            List<SelectListDto> attachments = (from x in _dbContext.CaseAttachments.Where(x => x.CaseId == currentHistry.CaseId)
                                               select new SelectListDto
                                               {
                                                   Id = x.Id,
                                                   Name = x.FilePath

                                               }).ToList();

            CaseEncodeGetDto result = new CaseEncodeGetDto
            {
                Id = currentHistry.Id,
                CaseTypeName = currentHistry.Case.CaseType.CaseTypeTitle,
                CaseNumber = currentHistry.Case.CaseNumber,
                CreatedAt = currentHistry.Case.CreatedAt.ToString(),
                ApplicantName = currentHistry.Case.Applicant.ApplicantName,
                ApplicantPhoneNo = currentHistry.Case.Applicant.PhoneNumber,
                EmployeeName = currentHistry.Case.Employee.FullName,
                EmployeePhoneNo = currentHistry.Case.Employee.PhoneNumber,
                LetterNumber = currentHistry.Case.LetterNumber,
                LetterSubject = currentHistry.Case.LetterSubject,
                Position = user.Position.ToString(),
                FromStructure = currentHistry.FromStructure.StructureName,
                FromEmployeeId = currentHistry.FromEmployee.FullName,
                ReciverType = currentHistry.ReciverType.ToString(),
                SecreateryNeeded = currentHistry.SecreateryNeeded,
                IsConfirmedBySeretery = currentHistry.IsConfirmedBySeretery,
                ToEmployee = currentHistry.ToEmployee.FullName,
                ToStructure = currentHistry.ToStructure.StructureName,
                AffairHistoryStatus = currentHistry.AffairHistoryStatus.ToString(),
                Attachments = attachments

            };


            return result;







        }



    }
}

using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Case;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.CaseModel;

namespace PM_Case_Managemnt_API.Services.CaseMGMT
{
    public class CaserReportService : ICaseReportService
    {

        private readonly DBContext _dbContext;
        public CaserReportService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<CaseReportDto>> GetCaseReport()
        {
            
                var allAffairs = _dbContext.Cases.Include(a => a.CaseType)
                   .Include(a => a.CaseHistories);

                var report = new List<CaseReportDto>();
                foreach (var affair in allAffairs.ToList())
                {
                    var eachReport = new CaseReportDto();
                    eachReport.Id = affair.Id;
                    eachReport.CaseType = affair.CaseType.CaseTypeTitle;
                    eachReport.CaseNumber = affair.CaseNumber;
                    eachReport.Subject = affair.LetterSubject;
                    eachReport.IsArchived = affair.IsArchived.ToString();
                    var firstOrDefault = affair.CaseHistories.OrderByDescending(x => x.CreatedAt)
                        .FirstOrDefault();
                    if (firstOrDefault != null)
                        eachReport.OnStructure = _dbContext.OrganizationalStructures.Find(firstOrDefault
                                .ToStructureId).StructureName;
                    var affairHistory = affair.CaseHistories.OrderByDescending(x => x.CreatedAt)
                        .FirstOrDefault();
                    if (affairHistory != null)
                        eachReport.OnEmployee = _dbContext.Employees.Find(affairHistory
                                .ToEmployeeId).FullName;
                    eachReport.CaseStatus = affair.AffairStatus.ToString();
                    report.Add(eachReport);
                    eachReport.CreatedDateTime = affair.CreatedAt;
                    eachReport.CaseCounter = affair.CaseType.Counter;
                    var change = DateTime.Now.Subtract(eachReport.CreatedDateTime).TotalHours;
                    if (affair.AffairStatus == AffairStatus.Completed)
                    {
                        var completedAt =
                            affair.CaseHistories
                                .FirstOrDefault(x => x.AffairHistoryStatus == AffairHistoryStatus.Completed).CompletedDateTime;
                        if (completedAt != null)
                        {
                            change = completedAt.Value.Subtract(eachReport.CreatedDateTime).TotalHours;
                        }
                    }
                    var d = change;
                    d = Math.Round((Double)d, 2);
                    eachReport.ElapsTime = d;

                }
                //if (affairStatus != null)
                //{
                //    report = affairStatus == AffairStatus.Completed ? report.OrderBy(x => x.ElapsTime).ToList() : report.OrderByDescending(x => x.ElapsTime).ToList();
                //}
                var AllReport =  report.OrderByDescending(x => x.CreatedDateTime).ToList();
                return AllReport;



        }
    }
}

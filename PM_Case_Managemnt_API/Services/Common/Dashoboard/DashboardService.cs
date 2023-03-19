using PM_Case_Managemnt_API.Data;
using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.DTOS.Case;

namespace PM_Case_Managemnt_API.Services.Common.Dashoboard
{
    public class DashboardService: IDashboardService
    {

        private readonly DBContext _dBContext;
        public DashboardService(DBContext context)
        {
            _dBContext = context;
        }
        public async Task<DashboardDto> GetPendingCase(string startat, string endat)
        {

            var allAffairps = _dBContext.Cases
                 .Include(a => a.CaseType)
                 .Include(a => a.Applicant)
                .Include(a => a.CaseHistories)
                .Where(a =>
                a.CreatedAt.Month == DateTime.Now.Month);
            allAffairps = allAffairps.Where(x => x.AffairStatus != AffairStatus.Completed);

            //if (startAt != null)
            //    allAffairs = allAffairs.Where(x => x.CreatedDateTime >= startAt);
            //if (endAt != null)
            //    allAffairs = allAffairs.Where(x => x.CreatedDateTime <= endAt);

            var report = new List<TopAffairsViewmodel>();
            foreach (var affair in allAffairps.ToList())
            {
                var eachReport = new TopAffairsViewmodel();
                eachReport.AffairNumber = affair.CaseNumber;
                eachReport.ApplicantName = affair.Applicant?.ApplicantName;
                eachReport.Subject = affair.LetterSubject;
                var firstOrDefault = affair.CaseHistories.OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault();
                if (firstOrDefault != null)
                    eachReport.Structure = _dBContext.OrganizationalStructures.Find(firstOrDefault
                            .ToStructureId).StructureName;
                var affairHistory = affair.CaseHistories.OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault();
                if (affairHistory != null)
                    eachReport.Employee = _dBContext.Employees.Find(affairHistory
                            .ToEmployeeId).FullName;
                eachReport.CreatedDateTime = affair.CreatedAt;
                var change = DateTime.Now.Subtract(eachReport.CreatedDateTime).TotalHours;
                var d = change / 24;
                d = Math.Round((Double)d, 2);
                eachReport.Elapstime = d;
                eachReport.Level = (change * 100) / affair.CaseType.Counter;
                report.Add(eachReport);

            }
            report = report.OrderByDescending(x => x.Level).ToList();



            if (!string.IsNullOrEmpty(startat))
            {
                string[] startDate = startat.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                DateTime MDateTime = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(startDate[0]), Int32.Parse(startDate[1]), Int32.Parse(startDate[2])));
                report = report.Where(x => x.CreatedDateTime >= MDateTime).ToList();
            }

            if (!string.IsNullOrEmpty(endat))
            {

                string[] endDate = endat.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                DateTime MDateTime = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(endDate[0]), Int32.Parse(endDate[1]), Int32.Parse(endDate[2])));
                report = report.Where(x => x.CreatedDateTime <= MDateTime).ToList();
            }


            DashboardDto dashboard = new DashboardDto();
            dashboard.pendingReports = report;

            allAffairps = _dBContext.Cases
                .Include(a => a.CaseType)
                 .Include(a => a.Applicant)
                  .Include(a => a.Employee)
                .Include(a => a.CaseHistories)
                .Where(a =>
                a.CreatedAt.Month == DateTime.Now.Month);
            allAffairps = allAffairps.Where(x => x.AffairStatus == AffairStatus.Completed);

            report = new List<TopAffairsViewmodel>();
            foreach (var affair in allAffairps.ToList())
            {
                var eachReport = new TopAffairsViewmodel();
                eachReport.AffairNumber = affair.CaseNumber;
                eachReport.ApplicantName = affair.Applicant != null ? affair.Applicant.ApplicantName : affair.Employee.FullName;
                eachReport.Subject = affair.LetterSubject;
                var firstOrDefault = affair.CaseHistories.OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault();
                if (firstOrDefault != null)
                    eachReport.Structure = _dBContext.OrganizationalStructures.Find(firstOrDefault
                            .ToStructureId).StructureName;
                var affairHistory = affair.CaseHistories.OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault();
                if (affairHistory != null)
                    eachReport.Employee = _dBContext.Employees.Find(affairHistory
                            .ToEmployeeId).FullName;
                eachReport.CreatedDateTime = affair.CreatedAt;
                var change = firstOrDefault.CreatedAt.Subtract(eachReport.CreatedDateTime).TotalHours;
                var d = change / 24;
                d = Math.Round((Double)d, 2);
                eachReport.Elapstime = d;
                eachReport.Level = (change * 100) / affair.CaseType.Counter;
                report.Add(eachReport);

            }
            report = report.OrderByDescending(x => x.Level).ToList();

            if (!string.IsNullOrEmpty(startat))
            {
                string[] startDate = startat.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                DateTime MDateTime = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(startDate[0]), Int32.Parse(startDate[1]), Int32.Parse(startDate[2])));
                report = report.Where(x => x.CreatedDateTime >= MDateTime).ToList();
            }

            if (!string.IsNullOrEmpty(endat))
            {

                string[] endDate = endat.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                DateTime MDateTime = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(endDate[0]), Int32.Parse(endDate[1]), Int32.Parse(endDate[2])));
                report = report.Where(x => x.CreatedDateTime <= MDateTime).ToList();
            }

            dashboard.completedReports = report;

            return dashboard;

        }
    }
}

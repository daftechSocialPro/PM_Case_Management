using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Case;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.CaseModel;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace PM_Case_Managemnt_API.Services.CaseMGMT
{
    public class CaserReportService : ICaseReportService
    {

        private readonly DBContext _dbContext;
        private Random rnd = new Random();
        public CaserReportService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<CaseReportDto>> GetCaseReport(string? startAt, string? endAt)
        {
            
                var allAffairs = _dbContext.Cases.Include(a => a.CaseType)
                   .Include(a => a.CaseHistories).ToList();

            if (!string.IsNullOrEmpty(startAt))
            {
                string[] startDate = startAt.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                DateTime MDateTime = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(startDate[0]), Int32.Parse(startDate[1]), Int32.Parse(startDate[2])));
                allAffairs = allAffairs.Where(x => x.CreatedAt >= MDateTime).ToList();
            }

            if (!string.IsNullOrEmpty(endAt))
            {

                string[] endDate = endAt.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                DateTime MDateTime = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(endDate[0]), Int32.Parse(endDate[1]), Int32.Parse(endDate[2])));
                allAffairs = allAffairs.Where(x => x.CreatedAt <= MDateTime).ToList();
            }


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

        public async Task<CaseReportChartDto> GetCasePieChart(string? startAt, string? endAt)
        {
            var report = _dbContext.CaseTypes.ToList();
            var report2 = (from q in report
                           join
          b in _dbContext.Cases on q.Id equals b.CaseTypeId
                           select
                           new
                           {
                               q.CaseTypeTitle
                           }).Distinct();

            
            var Chart = new CaseReportChartDto();

            Chart.labels = new List<string>();
            Chart.datasets = new List<DataSets>();

            var datas = new DataSets();

            datas.data = new List<int>();
            datas.hoverBackgroundColor = new List<string>();
            datas.backgroundColor = new List<string>();



            foreach (var eachreport in report2)
            {

               

                var allAffairs = _dbContext.Cases.Where(x => x.CaseType.CaseTypeTitle == eachreport.CaseTypeTitle);
                var caseCount = allAffairs.Count();


                if (!string.IsNullOrEmpty(startAt))
                {
                    string[] startDate = startAt.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    DateTime MDateTime = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(startDate[0]), Int32.Parse(startDate[1]), Int32.Parse(startDate[2])));
                    allAffairs = allAffairs.Where(x => x.CreatedAt >= MDateTime);
                    caseCount = allAffairs.Count();


                }

                if (!string.IsNullOrEmpty(endAt))
                {

                    string[] endDate = endAt.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    DateTime MDateTime = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(endDate[0]), Int32.Parse(endDate[1]), Int32.Parse(endDate[2])));
                    allAffairs = allAffairs.Where(x => x.CreatedAt <= MDateTime);
                    caseCount = allAffairs.Count();
                   
                }

                Chart.labels.Add(eachreport.CaseTypeTitle);     

                datas.data.Add(caseCount);
                string randomColor = String.Format("#{0:X6}", rnd.Next(0x1000000));
                datas.backgroundColor.Add(randomColor);
                datas.hoverBackgroundColor.Add(randomColor);


                Chart.datasets.Add(datas);



            }
  

            return Chart;
        }

        public async Task<CaseReportChartDto> GetCasePieCharByCaseStatus(string? startAt, string? endAt)
        {


            var allAffairs = _dbContext.Cases.Where(x => x.CaseNumber != null);




          
            var caseCount = allAffairs.Count();


            if (!string.IsNullOrEmpty(startAt))
            {
                string[] startDate = startAt.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                DateTime MDateTime = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(startDate[0]), Int32.Parse(startDate[1]), Int32.Parse(startDate[2])));
                allAffairs = allAffairs.Where(x => x.CreatedAt >= MDateTime);
                caseCount = allAffairs.Count();


            }

            if (!string.IsNullOrEmpty(endAt))
            {

                string[] endDate = endAt.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                DateTime MDateTime = Convert.ToDateTime(XAPI.EthiopicDateTime.GetGregorianDate(Int32.Parse(endDate[0]), Int32.Parse(endDate[1]), Int32.Parse(endDate[2])));
                allAffairs = allAffairs.Where(x => x.CreatedAt <= MDateTime);
                caseCount = allAffairs.Count();

            }

            int assigned = allAffairs.Count(x => x.AffairStatus == AffairStatus.Assigned);
            int completed = allAffairs.Count(x => x.AffairStatus == AffairStatus.Completed);
            int encoded = allAffairs.Count(x => x.AffairStatus == AffairStatus.Encoded);
            int pend = allAffairs.Count(x => x.AffairStatus == AffairStatus.Pend);



            var Chart = new CaseReportChartDto();

            Chart.labels = new List<string>() { "Assigned", "completed", "Encoded", "Pend"};
            Chart.datasets = new List<DataSets>();

            var datas = new DataSets();

            datas.data = new List<int>() {assigned,completed,encoded,pend };
            datas.hoverBackgroundColor = new List<string>() { "#5591f5", "#2cb436", "#dfd02f", "#fe5e2b" };
            datas.backgroundColor = new List<string>() { "#5591f5", "#2cb436", "#dfd02f", "#fe5e2b" };

            Chart.datasets.Add(datas);

            return Chart;
        }
    }
}

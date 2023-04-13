using PM_Case_Managemnt_API.Data;
using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.DTOS.Case;
using System.Text;
using PM_Case_Managemnt_API.Models.Common;


namespace PM_Case_Managemnt_API.Services.Common.Dashoboard
{
    public class DashboardService: IDashboardService
    {

        private readonly DBContext _dBContext;
        private Random rnd = new Random();
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
                            .Include(a => a.Employee.OrganizationalStructure)
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
                eachReport.CaseTypeTitle = affair.CaseType.CaseTypeTitle;
                eachReport.AffairNumber = affair.CaseNumber;
                eachReport.ApplicantName = affair.Applicant?.ApplicantName;
                eachReport.Subject = affair.LetterSubject;
                var firstOrDefault = _dBContext.CaseHistories.Include(x=>x.ToStructure).Where(x=>x.CaseId==affair.Id).OrderByDescending(x => x.CreatedAt)
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
                eachReport.CaseTypeTitle = affair.CaseType.CaseTypeTitle;
                eachReport.AffairNumber = affair.CaseNumber;
                eachReport.ApplicantName = affair.Applicant != null ? affair.Applicant.ApplicantName : affair.Employee.FullName;
                eachReport.Subject = affair.LetterSubject;
                var firstOrDefault = _dBContext.CaseHistories.Include(x => x.ToStructure).Where(x => x.CaseId == affair.Id).OrderByDescending(x => x.CreatedAt)
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


            var Chart = new CaseReportChartDto();

            Chart.labels = new List<string>() { "LateProgress", "completed"};
            Chart.datasets = new List<DataSets>();

            var datas = new DataSets();

            datas.data = new List<int>() { dashboard.pendingReports.Count(), dashboard.completedReports.Count()};
            datas.hoverBackgroundColor = new List<string>() { "#fe5e2b", "#2cb436" };
            datas.backgroundColor = new List<string>() { "#fe5e2b", "#2cb436" };

            Chart.datasets.Add(datas);


            dashboard.chart = Chart;





            return dashboard;

        }



        public async Task<barChartDto> GetMonthlyReport()
        {

            barChartDto barChart = new barChartDto();
            barChart.labels = new List<string>() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "sep", "Oct", "Nov", "Dec" };
            barChart.datasets = new List<barChartDetailDto>();



            var allAffairs = _dBContext.Cases.Include(x=>x.CaseType).Where(x => x.CreatedAt.Year == DateTime.Now.Year).ToList();
            var allAffairTypes = _dBContext.CaseTypes.Where(x => x.RowStatus == RowStatus.Active && x.ParentCaseTypeId == null && x.CaseForm == CaseForm.Outside).ToList();
            var monthList = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            foreach (var affairType in allAffairTypes)
            {


                barChartDetailDto dataset = new barChartDetailDto
                {
                    type = "bar",
                    label = affairType.CaseTypeTitle,
                    backgroundColor = String.Format("#{0:X6}", rnd.Next(0x1000000))
                };

                dataset.data = new List<int>();

                foreach (var month in monthList)
                {

                    dataset.data.Add(
                        allAffairs.Count(x => x.CaseTypeId == affairType.Id && x.CreatedAt.Month == month && x.CaseType.ParentCaseTypeId == null));

                 
                }
                barChart.datasets.Add(dataset);


            }

            return barChart;

        }


        }
    }

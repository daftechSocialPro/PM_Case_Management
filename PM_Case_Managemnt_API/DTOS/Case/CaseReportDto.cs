using PM_Case_Managemnt_API.Models.CaseModel;
using System.Data;

namespace PM_Case_Managemnt_API.DTOS.Case
{
    public class CaseReportDto
    {
        public Guid Id { get; set; }
        public string CaseNumber { get; set; }
        public string CaseType { get; set; }
        public string Subject { get; set; }
        public string IsArchived { get; set; }
        public string OnStructure { get; set; }
        public string OnEmployee { get; set; }
        public string CaseStatus { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public float CaseCounter { get; set; }
        public double ElapsTime { get; set; }
    }

    public class CaseReportChartDto
    {

        public List<string> labels { get; set; }
        public List<DataSets> datasets { get; set; }

    }
    public class DataSets
    {

        public List<int> data { get; set; }
        public List<string> backgroundColor { get; set; }
        public List<string> hoverBackgroundColor { get; set; }


    }
    public class AffairAnalysis
    {
        public string AffairTypeTitle { get; set; }
        public string Remark { get; set; }
        public float Counter { get; set; }
        public float MeanTime { get; set; }
    }


    public class EmployeePerformance
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public string Image { get; set; }
        public string EmployeeStructure { get; set; }
        public float WorkeDonePercent { get; set; }
        public double ActualTimeTaken { get; set; }
        public double ExpectedTime { get; set; }
        public string PerformanceStatus { get; set; }

    }

    public class SMSReportDto
    {
        public string CaseNumber { get; set; }
        public string ApplicantName { get; set; }
        public string LetterNumber { get; set; }
        public string Subject { get; set; }
        public string CaseTypeTitle { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Message { get; set; }
        public string MessageGroup { get; set; }

        public bool IsSMSSent { get; set; }

        public DateTime CreatedAt { get; set; }



    }


    public class CaseDetailReportDto
    {
     public string CaseNumber { get; set; }
     public string ApplicantName { get; set;}

     public string LetterNumber { get; set;}

     public string Subject { get; set; }    

     public string CaseTypeTitle { get; set;}

     public string CaseTypeStatus { get; set; }

    public string PhoneNumber { get; set; }

    public string Createdat { get; set; }

    }


    public enum PerformanceStatus
    {
        OverPlan,
        OnPlan,
        UnderPlan
    }




}

using Microsoft.Identity.Client;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.DTOS.CaseDto
{
    public class CaseTypePostDto
    {
        public string CaseTypeTitle { get; set; } = null!;
        public string Code { get; set; } = null!;
        public float TotalPayment { get; set; }
        public float Counter { get; set; }
        public TimeMeasurement MeasurementUnit { get; set; }
        public CaseForm CaseForm { get; set; }
        public string Remark { get; set; }
        public Guid CreatedBy { get; set; }
        public int? OrderNumber { get; set; }
        public Guid? ParentCaseTypeId { get; set; }

    }

    public class CaseTypeGetDto
    {
        public Guid Id { get; set; }
        public string CaseTypeTitle { get; set; }
        public string Remark { get; set; }
        public float TotalPayment { get; set; }
        public RowStatus RowStatus { get; set; }
        public string Code { get; set; }
        public TimeMeasurement MeasurementUnit { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        //public Guid? ParentCaseTypeId { get; set; }
        public virtual CaseType ParentCaseType { get; set; } = null!;
    }
}
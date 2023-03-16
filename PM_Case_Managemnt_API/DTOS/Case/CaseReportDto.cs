using PM_Case_Managemnt_API.Models.CaseModel;

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
}

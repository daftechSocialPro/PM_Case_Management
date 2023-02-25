using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.DTOS.CaseDto
{
    public class AppointmentPostDto
    {
        public Guid CaseId { get; set; }
        public Guid EmployeeId { get; set; }
        public bool IsSmsSent { get; set; }
        public Guid CreatedBy { get; set; }
        public string Remark { get; set; }
    }
}

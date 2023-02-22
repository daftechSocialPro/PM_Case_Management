using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Models.CaseModel
{
    public class AppointemnetWithCalender : CommonModel
    {

        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } = null!;
        public DateTime AppointementDate { get; set; }
        public string Time { get; set; } = null!;
        public Guid CaseId { get; set; }
        public virtual Case Case { get; set; } = null!;

    }
}

using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Models.CaseModel
{
    public class Appointement : CommonModel
    {

        public Guid CaseId { get; set; }
        public virtual Case Case { get; set; }

        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public bool IsSmsSent { get; set; }

    }
}

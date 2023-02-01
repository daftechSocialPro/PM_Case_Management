
using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Models.PM
{
    public class TaskMemoReply : CommonModel
    {
        public Guid TaskMemoId { get; set; }
    //    public virtual TaskMemo TaskMemo { get; set; }

        public Guid EmployeeId { get; set; }
      //  public virtual Employee Employee { get; set; }

        public string Description { get; set; }
    }
}

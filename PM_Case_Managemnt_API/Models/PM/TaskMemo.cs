
using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Models.PM
{
    public class TaskMemo : CommonModel
    {

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public TaskMemo()
        //{
        //    Replies = new HashSet<TaskMemoReply>();

        //}
        public Guid? TaskId { get; set; }
        //public virtual Task Task { get; set; }

        public Guid? PlanId { get; set; }
       // public virtual Plan Plan { get; set; }


        public Guid? ActivityParentId { get; set; }
     //   public virtual ActivityParent ActivityParent { get; set; }


        public Guid EmployeeId { get; set; }
   //     public virtual Employee Employee { get; set; }

        public string Description { get; set; }
        // public virtual ICollection<TaskMemoReply> Replies { get; set; }
    }
}

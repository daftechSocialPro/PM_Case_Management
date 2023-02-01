

using PM_Case_Managemnt_API.Models.Common;
using System.ComponentModel;

namespace PM_Case_Managemnt_API.Models.PM
{
    public class ActivityTerminationHistories : CommonModel
    {
        public Guid ActivityId { get; set; }
      //  public virtual Activity Activity { get; set; }

        public Guid FromEmployeeId { get; set; }
      //  public virtual Employee FromEmployee { get; set; }

        public Guid? ToEmployeeId { get; set; }
     //   public virtual Employee ToEmployee { get; set; }

        public Guid? ToCommiteId { get; set; }
      //  public virtual Commitees ToCommite { get; set; }

        public string TerminationReason { get; set; }

        public Guid ApprovedByDirectorId { get; set; }
        public virtual Employee ApprovedByDirector { get; set; }

        public string DocumentPath { get; set; }
        [DefaultValue(false)]
        public Boolean isapproved { get; set; }
        [DefaultValue(false)]
        public Boolean isRejected { get; set; }

    }
}

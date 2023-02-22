
using PM_Case_Managemnt_API.Models.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace PM_Case_Managemnt_API.Models.PM
{
    public class Task : CommonModel
    {

        //public Task()
        //{
        //    TaskMemos = new HashSet<TaskMemo>();
        //    TaskMember = new HashSet<TaskMembers>();
        //    ActivitiesParents = new HashSet<ActivityParent>();
        //    Activities = new HashSet<Activity>();
        //}

        public Guid? PlanId { get; set; }
        public virtual Plan Plan { get; set; }
        public string TaskDescription { get; set; }
        //public Guid? TaskId { get; set; }
        //public virtual Task Task { get; set; }
        public DateTime? ShouldStartPeriod { get; set; }
        public DateTime? ActuallStart { get; set; }
        public DateTime? ShouldEnd { get; set; }
        public DateTime? ActualEnd { get; set; }
        public float PlanedBudget { get; set; }
        public float? ActualBudget { get; set; }
        public float? Goal { get; set; }
        public float? Weight { get; set; }
        public float ActualWorked { get; set; }
        public Status Status { get; set; }
        [DefaultValue(true)]
        public bool HasActivityParent { get; set; }


    }
    public enum ReportLevel
    {
        Structure,
        Employee
    }
}

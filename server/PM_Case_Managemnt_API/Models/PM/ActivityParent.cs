
using PM_Case_Managemnt_API.Models.Common;
using System.ComponentModel.DataAnnotations;


namespace PM_Case_Managemnt_API.Models.PM
{
    public class ActivityParent : CommonModel
    {

        //public ActivityParent()
        //{

        //    Activities = new HashSet<Activity>();
        //    TaskMemos = new HashSet<TaskMemo>();
        //    TaskMember = new HashSet<TaskMembers>();

        //}

        public Guid? TaskId { get; set; }
    //  public virtual Task Task { get; set; }
        public string ActivityParentDescription { get; set; }
        public DateTime? ShouldStartPeriod { get; set; }
        public DateTime? ActuallStart { get; set; }
        public DateTime? ShouldEnd { get; set; }
        public DateTime? ActualEnd { get; set; }
        public float PlanedBudget { get; set; }
        public float? ActualBudget { get; set; }

        //public SiUnites? UnitOfMeasurement { get; set; }
        public float? Goal { get; set; }
        public float? Weight { get; set; }
        public float ActualWorked { get; set; }
        public Status Status { get; set; }
        public bool HasActivity { get; set; }




    }
}

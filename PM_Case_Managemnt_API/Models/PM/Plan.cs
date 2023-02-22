
using PM_Case_Managemnt_API.Models.Common;
using System.ComponentModel;

namespace PM_Case_Managemnt_API.Models.PM
{
    public class Plan : CommonModel
    {

        //{
        //    Tasks = new HashSet<Taske>();
        //    TaskMemos = new HashSet<TaskMemo>();
        //    TaskMember = new HashSet<TaskMembers>();
        //    Activities = new HashSet<Activity>();
        //}



        public string PlanName { get; set; }
        public Guid? BudgetYearId { get; set; }
        public virtual BudgetYear BudgetYear { get; set; }
      // public virtual BudgetYear BudgetYear { get; set; }

        public Guid StructureId { get; set; }
        public virtual OrganizationalStructure Structure { get; set; }


        public DateTime? PeriodStartAt { get; set; }
        public DateTime? PeriodEndAt { get; set; }

        public Guid ProjectManagerId { get; set; }
        public virtual Employee ProjectManager { get; set; }

        
     
     //   public Guid ProjectCordinatorId { get; set; }
      
        public Guid FinanceId { get; set; }
        public virtual Employee Finance { get; set; }
       
        public Guid? ProgramId { get; set; }
        public virtual Programs Program { get; set; }

        public float PlanWeight { get; set; }

        [DefaultValue(true)]
        public bool HasTask { get; set; }
        public float PlandBudget { get; set; }

        public ProjectType ProjectType { get; set; }

    }

    public enum ProjectType
    {
        Capital,
        Regular
    }
}

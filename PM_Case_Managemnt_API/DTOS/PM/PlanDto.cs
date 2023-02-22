using PM_Case_Managemnt_API.DTOS.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PM_Case_Managemnt_API.DTOS.PM
{
    public class PlanDto
    {
        public Guid BudgetYearId { get; set; }
        public bool HasTask { get; set; }
        public string PlanName { get; set; }
        public float PlanWeight { get; set; }
        public float PlandBudget { get; set; }
        public Guid ProgramId { get; set; }
        public int ProjectType { get; set; }
        public string Remark { get; set; }
        public Guid StructureId { get; set; }
        public Guid ProjectManagerId { get; set; }
        public Guid FinanceId { get; set; }

    }

    public class PlanViewDto
    {
        public Guid Id { get; set; }
        public string PlanName { get; set; }
        public float PlanWeight { get; set; }
        public float PlandBudget { get; set; }
        public float RemainingBudget { get; set; }
        public string ProjectManager { get; set; }
        public string FinanceManager { get; set; }

        public string Director { get; set; }
        public string StructureName { get; set; }
        public string ProjectType { get; set; }

        public int NumberOfTask { get; set; }
        public int NumberOfActivities { get; set; }
        public int NumberOfTaskCompleted { get; set; }

        public bool HasTask { get; set; }

    }

    public class PlanSingleViewDto
    {
        public Guid Id { get; set; }
        public string PlanName { get; set; }
        public float PlanWeight { get; set; }

        public float RemainingWeight { get; set; }
        public float PlannedBudget { get; set; }
        public float RemainingBudget { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<TaskVIewDto> Tasks { get; set; }

    }

    public class TaskVIewDto
    {
        public Guid Id { get; set; }

        public string TaskName { get; set; }

        public float? TaskWeight { get; set; }

        public int NumberofActivities { get; set; }

        public int FinishedActivitiesNo { get; set; }

        public int TerminatedActivitiesNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int NumberOfMembers { get; set; }

        public List<SelectListDto> TaskMembers { get; set; }
        public List<TaskMemoDto> TaskMemos { get; set; }

        public List<ActivityViewDto> ActivityViewDtos { get; set; }

        public bool HasActivity { get; set; }

        public float PlannedBudget { get; set; }


    }

    public class TaskDto
    {


        public string TaskDescription { get; set; }

        public bool HasActvity { get; set; }

        public float PlannedBudget { get; set; }

        public Guid PlanId { get; set; }

    }

    public class TaskMembersDto
    {
        public SelectListDto[] Employee { get; set; }
        public Guid TaskId { get; set; }
    }

    public class TaskMemoDto 
        {
        public SelectListDto Employee { get; set; }
        public string  Description { get; set; }

        public DateTime DateTime { get; set; }
    }
    public class TaskMemoRequestDto
    {
      public Guid   EmployeeId { get; set; }
      public string Description { get; set; }
      public Guid TaskId { get; set; }
    }




}

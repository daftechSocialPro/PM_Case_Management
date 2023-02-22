using Microsoft.AspNetCore.Mvc.Rendering;
using PM_Case_Managemnt_API.DTOS.Common;


namespace PM_Case_Managemnt_API.DTOS.PM
{
    public class ActivityViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public float PlannedBudget { get; set; }
        public string ActivityType { get; set; } = null!;
        public float Weight { get; set; } 
        public float Begining { get; set; }
        public  float Target { get; set; }
        public string UnitOfMeasurment { get; set; } = null!;
        public float OverAllPerformance { get; set; }
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public List<SelectListDto> Members { get; set; } = null!;

        public List<MonthPerformanceViewDto>? MonthPerformance { get; set; } = null!;
    }

    public class MonthPerformanceViewDto
    {
        public string MonthName { get; set; } = null!;
        public decimal Planned { get; set; }
        public decimal Actual { get; set; }
        public float Percentage { get; set; }
    }
}
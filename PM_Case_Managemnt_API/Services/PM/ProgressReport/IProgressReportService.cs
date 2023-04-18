using PM_Case_Managemnt_API.DTOS.Common;
using static PM_Case_Managemnt_API.Services.PM.ProgressReport.ProgressReportService;

namespace PM_Case_Managemnt_API.Services.PM.ProgressReport
{
    public interface IProgressReportService
    {

        public Task<List<DiagramDto>> GetDirectorLevelPerformance(Guid? BranchId);
        public Task<PlanReportByProgramDto> PlanReportByProgram(string BudgetYear, string ReportBy);
    }
}

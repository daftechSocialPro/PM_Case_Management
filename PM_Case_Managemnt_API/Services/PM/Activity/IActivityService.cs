using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.DTOS.PM;
using PM_Case_Managemnt_API.Models.PM;
using PMCaseManagemntAPI.Migrations.DB;

namespace PM_Case_Managemnt_API.Services.PM.Activity
{
    public interface IActivityService
    {
        public Task<int> AddActivityDetails(ActivityDetailDto activityDetail);

        public Task<int> AddTargetActivities(ActivityTargetDivisionDto targetDivisions);

        public Task<int> AddProgress(AddProgressActivityDto activityProgress);

        public Task<List<ProgressViewDto>> ViewProgress(Guid actId);


        public Task<List<ActivityViewDto>> GetAssignedActivity(Guid employeeId);


        
        
        public Task <List<ActivityViewDto>> GetActivtiesForApproval (Guid employeeId);


        public Task<int> ApproveProgress(ApprovalProgressDto approvalProgressDto);
        
    }
}

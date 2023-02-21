using PM_Case_Managemnt_API.DTOS.PM;

namespace PM_Case_Managemnt_API.Services.PM.Activity
{
    public interface IActivityService
    {
        public Task<int> AddActivityDetails(ActivityDetailDto activityDetail);
    }
}

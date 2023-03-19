using PM_Case_Managemnt_API.DTOS.Case;

namespace PM_Case_Managemnt_API.Services.Common.Dashoboard
{
    public interface IDashboardService
    {

        public  Task<DashboardDto> GetPendingCase(string startat, string endat);
    }
}

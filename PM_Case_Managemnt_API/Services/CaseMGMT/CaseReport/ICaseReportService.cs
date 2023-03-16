using PM_Case_Managemnt_API.DTOS.Case;
using PM_Case_Managemnt_API.DTOS.CaseDto;

namespace PM_Case_Managemnt_API.Services.CaseMGMT
{
    public interface ICaseReportService
    {

        public Task<List<CaseReportDto>> GetCaseReport();

    }
}

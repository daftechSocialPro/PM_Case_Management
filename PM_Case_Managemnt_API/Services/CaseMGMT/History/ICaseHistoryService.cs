using PM_Case_Managemnt_API.DTOS.CaseDto;

namespace PM_Case_Managemnt_API.Services.CaseMGMT.History
{
    public interface ICaseHistoryService
    {
        public Task AddCaseHistory(CaseHistoryPostDto caseHistoryPost);
    }
}

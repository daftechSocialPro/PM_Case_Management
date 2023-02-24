using PM_Case_Managemnt_API.DTOS.Case;

namespace PM_Case_Managemnt_API.Services.CaseMGMT.History
{
    public interface ICaseHistoryService
    {
        public Task AddCaseHistory(CaseHistoryPostDto caseHistoryPost);
    }
}

using PM_Case_Managemnt_API.Models.CaseModel;

namespace PM_Case_Managemnt_API.Services.CaseMGMT.CaseAttachments
{
    public interface ICaseAttachementService
    {
        public Task AddAttachemnts(List<CaseAttachment> caseAttachments);
        public Task<List<CaseAttachment>> GetAttachements(string CaseId = null);
    }
}

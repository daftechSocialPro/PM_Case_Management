using PM_Case_Managemnt_API.DTOS.Case;

namespace PM_Case_Managemnt_API.Services.CaseMGMT.Applicants
{
    public interface IApplicantService
    {
        public Task AddApplicant(ApplicantPostDto applicant);
        public Task<List<ApplicantGetDto>> GetAll();
    }
}

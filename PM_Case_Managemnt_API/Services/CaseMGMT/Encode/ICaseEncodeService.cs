using PM_Case_Managemnt_API.DTOS.CaseDto;

namespace PM_Case_Managemnt_API.Services.CaseService.Encode
{
    public interface ICaseEncodeService
    {
        public Task<string> AddCaseEncoding(CaseEncodePostDto caseEncodePostDto);
        public Task<List<CaseEncodeGetDto>> GetCaseEncodings();
        public Task AssignTask(CaseAssignDto caseAssignDto);
    }
}

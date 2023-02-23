using PM_Case_Managemnt_API.DTOS.CaseDto;

namespace PM_Case_Managemnt_API.Services.CaseService.Encode
{
    public interface ICaseEncodeService
    {
        public Task AddCaseEncoding(CaseEncodePostDto caseEncodePostDto);
    }
}

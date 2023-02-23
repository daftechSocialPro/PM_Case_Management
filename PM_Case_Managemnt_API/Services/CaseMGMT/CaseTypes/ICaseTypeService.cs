using PM_Case_Managemnt_API.DTOS.CaseDto;

namespace PM_Case_Managemnt_API.Services.CaseService.CaseTypes
{
    public interface ICaseTypeService
    {
        public Task AddNewCaseType(CaseTypePostDto caseTypeDto);
        public Task<List<CaseTypeGetDto>> GetAllCaseTypes();
    }
}

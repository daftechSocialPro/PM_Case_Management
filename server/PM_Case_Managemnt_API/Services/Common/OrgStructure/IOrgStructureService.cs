using PM_Case_Managemnt_API.DTOS.Common;

namespace PM_Case_Managemnt_API.Services.Common
{
    public interface IOrgStructureService
    {

        public Task<int> CreateOrganizationalStructure(OrgStructureDto orgStructure);

        //public Task<int> UpdateOrganizationalProfile(OrganizationProfile organizationProfile);
        public Task<List<OrgStructureDto>> GetOrganizationStructures();

        public Task<List<SelectListDto>> getParentStrucctureSelectList(Guid branchId);


    }
}

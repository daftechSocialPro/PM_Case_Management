using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Services.Common
{
    public class OrgStructureService : IOrgStructureService
    {
        private readonly DBContext _dBContext;
        public OrgStructureService(DBContext context)
        {
            _dBContext = context;
        }

        public async Task<int> CreateOrganizationalStructure(OrgStructureDto orgStructure)
        {



            var orgStructure2 = new OrganizationalStructure
            {
                Id = Guid.NewGuid(),
                OrganizationBranchId = orgStructure.OrganizationBranchId,
                ParentStructureId = orgStructure.ParentStructureId,
                StructureName = orgStructure.StructureName,
                Order = orgStructure.Order,
                Weight = orgStructure.Weight,
                Remark = orgStructure.Remark,
                CreatedAt = DateTime.Now,

            };


            await _dBContext.AddAsync(orgStructure2);
            await _dBContext.SaveChangesAsync();

            return 1;

        }
        public async Task<List<OrgStructureDto>> GetOrganizationStructures()


        {


            List<OrgStructureDto> structures = await (from x in _dBContext.OrganizationalStructures.Include(x => x.OrganizationBranch).Include(x => x.ParentStructure)
                                                      select new OrgStructureDto
                                                      {
                                                          BranchName = x.OrganizationBranch.Name + (x.OrganizationBranch.IsHeadOffice ? " ( Head Office )" : ""),
                                                          OrganizationBranchId = x.OrganizationBranch.Id,
                                                          ParentStructureName = x.ParentStructure.StructureName,
                                                          StructureName = x.StructureName,
                                                          Order = x.Order,
                                                          Weight = x.Weight,
                                                          Remark = x.Remark

                                                      }).ToListAsync();



            return structures;
        }

        public async Task<List<SelectListDto>> getParentStrucctureSelectList(Guid branchId)
        {

            List<SelectListDto> list = await (from x in _dBContext.OrganizationalStructures.Where(y => y.OrganizationBranchId == branchId)
                                              select new SelectListDto
                                              {
                                                  Id = x.Id,
                                                  Name = x.StructureName

                                              }).ToListAsync();

            return list;
        }



        public async Task<int> UpdateOrganizationalStructure(OrgStructureDto orgStructure)
        {

            var orgStructure2 = _dBContext.OrganizationalStructures.Find(orgStructure.Id);

            orgStructure2.OrganizationBranchId = orgStructure.OrganizationBranchId;
            orgStructure2.ParentStructureId = orgStructure.ParentStructureId;
            orgStructure2.StructureName = orgStructure.StructureName;
            orgStructure2.Order = orgStructure.Order;
            orgStructure2.Weight = orgStructure.Weight;
            orgStructure2.Remark = orgStructure.Remark;
            orgStructure2.RowStatus = orgStructure.RowStatus == 0 ? RowStatus.Active : RowStatus.InActive;

            _dBContext.Entry(orgStructure2).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();
            return 1;

        }



    }
}

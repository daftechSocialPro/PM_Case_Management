﻿using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.Common;


namespace PM_Case_Managemnt_API.Services.Common
{
    public class OrgBranchService : IOrgBranchService
    {

        private readonly DBContext _dBContext;
        public OrgBranchService(DBContext context)
        {
            _dBContext = context;
        }

        public async Task<int> CreateOrganizationalBranch(OrgBranchDto organizationBranch)
        {

            var orgProfile = _dBContext.OrganizationProfile.FirstOrDefault();

            if (orgProfile != null)
            {
                var orgBranch = new OrganizationBranch
                {
                    Id = Guid.NewGuid(),
                    Name = organizationBranch.Name,
                    Address = organizationBranch.Address,
                    PhoneNumber = organizationBranch.PhoneNumber,
                    Remark = organizationBranch.Remark,
                    CreatedAt = DateTime.Now,
                    OrganizationProfileId = orgProfile.Id
                };
                await _dBContext.AddAsync(orgBranch);
                await _dBContext.SaveChangesAsync();
            }
            return 1;

        }
        public async Task<List<OrganizationBranch>> GetOrganizationBranches()
        {
            return await _dBContext.OrganizationBranches.Where(x => !x.IsHeadOffice).Include(x => x.OrganizationProfile).ToListAsync();
        }

        public async Task<List<SelectListDto>> getBranchSelectList()
        {

            List<SelectListDto> list = await (from x in _dBContext.OrganizationBranches
                                              select new SelectListDto
                                              {
                                                  Id = x.Id,
                                                  Name = x.Name + (x.IsHeadOffice ? "( Head Office )" : "")

                                              }).ToListAsync();


            return list;
        }

        //public async Task<int> UpdateOrganizationalProfile(OrganizationProfile organizationProfile)
        //{

        //    _dBContext.Entry(organizationProfile).State = EntityState.Modified;
        //    await _dBContext.SaveChangesAsync();
        //    return 1;

        //}
    }
}

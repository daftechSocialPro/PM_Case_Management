using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.DTOS.PM;
using PM_Case_Managemnt_API.Models.PM;

namespace PM_Case_Managemnt_API.Services.PM.Commite
{
    public class CommiteService: ICommiteService
    {
        private readonly DBContext _dBContext;
        public CommiteService(DBContext context)
        {
            _dBContext = context;
        }

        public async Task<int> AddCommite(AddCommiteDto addCommiteDto)
        {
            var Commite = new Commitees
            {
                Id = Guid.NewGuid(),
                CommiteeName = addCommiteDto.Name,
                CreatedAt = DateTime.Now,
                CreatedBy = addCommiteDto.CreatedBy,
                Remark = addCommiteDto.Remark,
                RowStatus = Models.Common.RowStatus.Active
            };
            await _dBContext.AddAsync(Commite);
            await _dBContext.SaveChangesAsync();
            return 1;
        }

        public async Task<List<CommiteListDto>> GetCommiteLists()
        {
            var commites = await (from t in _dBContext.Commitees.AsNoTracking()
                         select new CommiteListDto
                         {
                             Id = t.Id,
                             Name= t.CommiteeName,
                             NoOfEmployees = t.Employees.Count(),
                             EmployeeList = t.Employees.Select(e => new SelectListDto
                             {
                                 Name = e.Employee.FullName,
                                 Id= e.Id,
                             }).ToList()
                         }).ToListAsync();

            return commites;
        }

        public async Task<List<SelectListDto>> GetNotIncludedEmployees(Guid CommiteId)
        {
            var EmployeeSelectList = await (from e in _dBContext.Employees
                                            where !(_dBContext.CommiteEmployees.Where(x => x.CommiteeId.Equals(CommiteId)).Select(x => x.EmployeeId).Contains(e.Id))
                                            select new SelectListDto
                                            {
                                                Id = e.Id,
                                                Name = e.FullName

                                            }).ToListAsync();

            return EmployeeSelectList;
        }

        public async Task<int> UpdateCommite(UpdateCommiteDto updateCommite)
        {
            var currentCommite = await _dBContext.Commitees.FirstOrDefaultAsync(x => x.Id.Equals(updateCommite.Id));
            if (currentCommite != null)
            {
                currentCommite.CommiteeName = updateCommite.Name;
                currentCommite.Remark = updateCommite.Remark;
                currentCommite.RowStatus = updateCommite.RowStatus;
                await _dBContext.SaveChangesAsync();

                return 1;
            }
            return 0;
        }
    }
}

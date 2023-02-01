using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common;

using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Services.Common
{
    public class BudgetYearService : IBudgetyearService
    {
        private readonly DBContext _dBContext;
        public BudgetYearService(DBContext context)
        {
            _dBContext = context;
        }

        public async Task<int> CreateProgramBudgetYear(ProgramBudgetYear programBudgetYear)
        {

            programBudgetYear.Id= Guid.NewGuid();
            

            await _dBContext.AddAsync(programBudgetYear);
            await _dBContext.SaveChangesAsync();

            return 1;

        }
        public async Task<List<ProgramBudgetYear>> GetProgramBudgetYears()
        {
            return await _dBContext.ProgramBudgetYears.Include(x=>x.BudgetYears).ToListAsync();
        }

        public async Task<List<SelectListDto>> getProgramBudgetSelectList()
        {

            List<SelectListDto> list = await (from x in _dBContext.ProgramBudgetYears
                                              select new SelectListDto
                                              {
                                                  Id = x.Id,
                                                  Name = x.Name

                                              }).ToListAsync();


            return list;
        }


        //budget year
        public async Task<int> CreateBudgetYear(PM_Case_Managemnt_API.Models.Common.BudgetYear BudgetYear)
        {

            BudgetYear.Id = Guid.NewGuid();
            await _dBContext.AddAsync(BudgetYear);
            await _dBContext.SaveChangesAsync();

            return 1;

        }



        public async Task<List<PM_Case_Managemnt_API.Models.Common.BudgetYear>> GetBudgetYears(Guid programBudgetYearId)
        {
            return await _dBContext.BudgetYears.Where(x=>x.ProgramBudgetYearId==programBudgetYearId).ToListAsync();
        }

        public async Task<List<SelectListDto>> getBudgetSelectList()
        {

            List<SelectListDto> list = await (from x in _dBContext.BudgetYears
                                              select new SelectListDto
                                              {
                                                  Id = x.Id,
                                                  Name = x.Year.ToString() + " ("  + " ) ( " +x.RowStatus + ")"

                                              }).ToListAsync();


            return list;
        }



    }
}

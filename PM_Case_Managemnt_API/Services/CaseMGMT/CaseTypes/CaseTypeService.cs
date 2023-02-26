using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.CaseModel;

namespace PM_Case_Managemnt_API.Services.CaseService.CaseTypes
{

    public class CaseTypeService: ICaseTypeService
    {

        private readonly DBContext _dbContext;
        public CaseTypeService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(CaseTypePostDto caseTypeDto)
        {
            try
            {
                CaseType caseType = new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    RowStatus = Models.Common.RowStatus.Active,
                    CreatedBy = caseTypeDto.CreatedBy,
                    CaseTypeTitle = caseTypeDto.CaseTypeTitle,
                    Code = caseTypeDto.Code,
                    TotlaPayment = caseTypeDto.TotalPayment,
                    Counter = caseTypeDto.Counter,
                    MeasurementUnit = Enum.Parse<TimeMeasurement>(caseTypeDto.MeasurementUnit),
                    CaseForm = Enum.Parse<CaseForm>(caseTypeDto.CaseForm),
                    Remark = caseTypeDto.Remark,
                    OrderNumber = caseTypeDto.OrderNumber,
                    ParentCaseTypeId = null
            };
                
                if (caseTypeDto.ParentCaseTypeId != null)
                    caseType.ParentCaseTypeId = caseTypeDto.ParentCaseTypeId;

                await _dbContext.AddAsync(caseType);
                await _dbContext.SaveChangesAsync();

            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CaseTypeGetDto>> GetAll()
        {
            try
            {
                List<CaseType> caseTypes = await _dbContext.CaseTypes.Include(p => p.ParentCaseType).ToListAsync();
                List<CaseTypeGetDto> result = new();

                foreach (CaseType caseType in caseTypes) { 
                    result.Add(new CaseTypeGetDto
                    {
                        Id = caseType.Id,
                        CaseTypeTitle = caseType.CaseTypeTitle,
                        Code = caseType.Code,
                        CreatedAt = caseType.CreatedAt.ToString(),
                        CreatedBy = caseType.CreatedBy,
                        MeasurementUnit = caseType.MeasurementUnit.ToString(),
                        Remark = caseType.Remark,
                        RowStatus = caseType.RowStatus.ToString(),
                        TotalPayment = caseType.TotlaPayment,
                        //ParentCaseType = caseType.ParentCaseType
                    });
                }

                return result;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<SelectListDto>> GetAllByCaseForm(string caseForm )
        {
            try
            {
                List<CaseType> caseTypes = await _dbContext.CaseTypes.Include(p => p.ParentCaseType).Where(x=>x.CaseForm==Enum.Parse<CaseForm>(caseForm)).ToListAsync();
                List<SelectListDto> result = new();

                foreach (CaseType caseType in caseTypes)
                {
                    result.Add(new SelectListDto
                    {
                        Id = caseType.Id,
                        Name = caseType.CaseTypeTitle,
                      
                      
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<SelectListDto>> GetAllSelectList()
        {

            return await (from c in _dbContext.CaseTypes
                          select new SelectListDto
                          {
                              Id= c.Id,
                              Name= c.CaseTypeTitle

                          }).ToListAsync();

        }

    }
}

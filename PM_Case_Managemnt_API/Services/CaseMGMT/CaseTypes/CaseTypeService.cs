using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.CaseDto;
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

        public async Task AddNewCaseType(CaseTypePostDto caseTypeDto)
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
                    MeasurementUnit = caseTypeDto.MeasurementUnit,
                    CaseForm = caseTypeDto.CaseForm,
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

        public async Task<List<CaseTypeGetDto>> GetAllCaseTypes()
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
                        CreatedAt = caseType.CreatedAt,
                        CreatedBy = caseType.CreatedBy,
                        MeasurementUnit = caseType.MeasurementUnit,
                        Remark = caseType.Remark,
                        RowStatus = caseType.RowStatus,
                        TotalPayment = caseType.TotlaPayment,
                        ParentCaseType = caseType.ParentCaseType
                    });
                }

                return result;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }


    }
}

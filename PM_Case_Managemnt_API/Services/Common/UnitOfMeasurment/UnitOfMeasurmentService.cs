
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.Common;
using PM_Case_Managemnt_API.Data;
using Microsoft.EntityFrameworkCore;

namespace PM_Case_Managemnt_API.Services.Common
{
    public class UnitOfMeasurmentService : IUnitOfMeasurmentService
    {


        private readonly DBContext _dBContext;
        public UnitOfMeasurmentService(DBContext context)
        {
            _dBContext = context;
        }

        public async Task<int> CreateUnitOfMeasurment(PM_Case_Managemnt_API.Models.Common.UnitOfMeasurment unitOfMeasurment)
        {

            unitOfMeasurment.Id = Guid.NewGuid();
            unitOfMeasurment.Name = unitOfMeasurment.Name;
            unitOfMeasurment.LocalName = unitOfMeasurment.LocalName;
            unitOfMeasurment.CreatedAt = DateTime.Now;


            await _dBContext.AddAsync(unitOfMeasurment);
            await _dBContext.SaveChangesAsync();

            return 1;

        }
        public async Task<List<PM_Case_Managemnt_API.Models.Common.UnitOfMeasurment>> GetUnitOfMeasurment()
        {
            return await _dBContext.UnitOfMeasurment.ToListAsync();
        }

        public async Task<List<SelectListDto>> getUnitOfMeasurmentSelectList()
        {

            List<SelectListDto> list = await (from x in _dBContext.UnitOfMeasurment
                                              select new SelectListDto
                                              {
                                                  Id = x.Id,
                                                  Name = x.Name + (x.LocalName)

                                              }).ToListAsync();


            return list;
        }

        public async Task<int> UpdateUnitOfMeasurment(PM_Case_Managemnt_API.Models.Common.UnitOfMeasurment unitOfMeasurment)
        {

            _dBContext.Entry(unitOfMeasurment).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();
            return 1;

        }
    }
}

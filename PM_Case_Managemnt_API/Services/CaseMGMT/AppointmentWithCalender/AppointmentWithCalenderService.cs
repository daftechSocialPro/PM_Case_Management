using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.CaseDto;
using PM_Case_Managemnt_API.Models.CaseModel;
using PM_Case_Managemnt_API.Models.Common;

namespace PM_Case_Managemnt_API.Services.CaseMGMT.AppointmentWithCalenderService
{
    public class AppointmentWithCalenderService: IAppointmentWithCalenderService
    {
        private readonly DBContext _dbContext;


        public AppointmentWithCalenderService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(AppointmentWithCalenderPostDto appointmentWithCalender)
        {
            try
            {
                AppointementWithCalender appointment = new()
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = appointmentWithCalender.CreatedBy,
                    AppointementDate = appointmentWithCalender.AppointementDate,
                    CaseId = appointmentWithCalender.CaseId,
                    EmployeeId = appointmentWithCalender.EmployeeId,
                    RowStatus = RowStatus.Active,
                    Remark = appointmentWithCalender.Remark,
                    Time = appointmentWithCalender.Time,
                };

                await _dbContext.AppointementWithCalender.AddAsync(appointment);
                await _dbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AppointementWithCalender>> GetAll()
        {
            try
            {
                return await _dbContext.AppointementWithCalender.Include(appointment => appointment.Employee).Include(appointment => appointment.Case).ToListAsync();
            } catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

    }
}

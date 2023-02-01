using PM_Case_Managemnt_API.DTOS.Common;


namespace PM_Case_Managemnt_API.Services.Common
{
    public interface IEmployeeService
    {

        public Task<int> CreateEmployee(EmployeeDto employee);
        public Task<int> UpdateEmployee(EmployeeDto employee);
        public Task<List<EmployeeDto>> GetEmployees();


    }
}

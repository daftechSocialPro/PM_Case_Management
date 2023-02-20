using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.Common;
using PMCaseManagemntAPI.Migrations;


namespace PM_Case_Managemnt_API.Services.Common
{
    public class EmployeeService : IEmployeeService
    {

        private readonly DBContext _dBContext;
        private readonly AuthenticationContext _authentication;
        public EmployeeService(DBContext context, AuthenticationContext authentication)
        {
            _dBContext = context;
            _authentication = authentication;
        }

        public async Task<int> CreateEmployee(EmployeeDto employee)
        {
            try
            {

               Employee employee1 = new Employee
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    Photo = employee.Photo,
                    FullName = employee.FullName,
                    Title = employee.Title,
                    PhoneNumber = employee.PhoneNumber,
                    Gender = Enum.Parse<Gender>(employee.Gender),
                    Remark = employee.Remark,

                };





                await _dBContext.AddAsync(employee1);

                var empstructure = new EmployeeStructures
                {
                    EmployeeId = employee1.Id,
                    OrganizationalStructureId = Guid.Parse(employee.StructureId),
                    Position = Enum.Parse<Position>(employee.Position),
                };

                await _dBContext.AddAsync(empstructure);



                await _dBContext.SaveChangesAsync();
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public async Task<List<SelectListDto>> GetEmployeesNoUserSelectList()
        {
            var emp = _authentication.ApplicationUsers.Select(x => x.EmployeesId).ToList();

           var EmployeeSelectList = await (from e in _dBContext.Employees where !(emp.Contains(e.Id))
                          select new SelectListDto
                          {
                              Id= e.Id,
                              Name = e.FullName

                          }).ToListAsync();

            return EmployeeSelectList;

        }


     
        public async Task<List<EmployeeDto>> GetEmployees()
        {



           



            var k=  await (from e in _dBContext.Employees
                          join es in _dBContext.EmployeesStructures.Include(x => x.OrganizationalStructure) on e.Id equals es.EmployeeId

                          select new EmployeeDto
                          {
                              Id= e.Id,
                              Photo = e.Photo,
                              Title = e.Title,
                              FullName = e.FullName,
                              Gender = e.Gender.ToString(),
                              PhoneNumber = e.PhoneNumber,
                              Position = es.Position.ToString(),
                              StructureName = es.OrganizationalStructure.StructureName,
                              BranchId = es.OrganizationalStructure.OrganizationBranchId.ToString(),
                              StructureId = es.OrganizationalStructureId.ToString(),
                              Remark = e.Remark

                          }).ToListAsync();

            return k;
        }

        public async Task<List<SelectListDto>> GetEmployeesSelectList()
        {
            var EmployeeSelectList = await (from e in _dBContext.Employees
                                          
                                            select new SelectListDto
                                            {
                                                Id = e.Id,
                                                Name = e.FullName

                                            }).ToListAsync();

            return EmployeeSelectList;



        }











        public async Task<int> UpdateEmployee(EmployeeDto employeeDto)
        {


            var orgEmployee = _dBContext.Employees.Find(employeeDto.Id);
            var orgEmployeeStructure  = _dBContext.EmployeesStructures.Where(x=>x.EmployeeId == employeeDto.Id).ToList().FirstOrDefault();

            orgEmployee.Photo = employeeDto.Photo;
            orgEmployee.Title = employeeDto.Title;
            orgEmployee.FullName = employeeDto.FullName;
            orgEmployee.Gender = Enum.Parse<Gender>( employeeDto.Gender);
            orgEmployee.PhoneNumber = employeeDto.PhoneNumber;
            orgEmployee.Remark = employeeDto.Remark;
            orgEmployeeStructure.Position = Enum.Parse<Position>( employeeDto.Position);
            orgEmployeeStructure.OrganizationalStructureId = Guid.Parse(employeeDto.StructureId);
            orgEmployee.RowStatus = employeeDto.RowStatus==0?RowStatus.Active:RowStatus.InActive;




            _dBContext.Entry(orgEmployeeStructure).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();

            _dBContext.Entry(orgEmployee).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();
            return 1;

        }

    }
}

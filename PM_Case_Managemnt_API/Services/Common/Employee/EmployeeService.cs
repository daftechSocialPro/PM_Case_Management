using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.Common;



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
                   OrganizationalStructureId = Guid.Parse(employee.StructureId),
                   Position = Enum.Parse<Position>(employee.Position),

               };





                await _dBContext.AddAsync(employee1);

       


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

            var EmployeeSelectList = await (from e in _dBContext.Employees
                                            where !(emp.Contains(e.Id))
                                            select new SelectListDto
                                            {
                                                Id = e.Id,
                                                Name = e.FullName

                                            }).ToListAsync();

            return EmployeeSelectList;

        }


     
        public async Task<List<EmployeeDto>> GetEmployees()
        {

            var k=  await (from e in _dBContext.Employees.Include(x=>x.OrganizationalStructure)
                         
                          select new EmployeeDto
                          {
                              Id= e.Id,
                              Photo = e.Photo,
                              Title = e.Title,
                              FullName = e.FullName,
                              Gender = e.Gender.ToString(),
                              PhoneNumber = e.PhoneNumber,
                              Position = e.Position.ToString(),
                              StructureName = e.OrganizationalStructure.StructureName,
                              BranchId = e.OrganizationalStructure.OrganizationBranchId.ToString(),
                              StructureId = e.OrganizationalStructureId.ToString(),
                              Remark = e.Remark

                          }).ToListAsync();

            return k;
        }

        public async Task<EmployeeDto> GetEmployeesById(Guid employeeId)
        {
            
            var k = await (from e in _dBContext.Employees.Include(x => x.OrganizationalStructure.OrganizationBranch).Where(x=>x.Id==employeeId)

                           select new EmployeeDto
                           {
                               Id = e.Id,
                               Photo = e.Photo,
                               Title = e.Title,
                               FullName = e.FullName,
                               Gender = e.Gender.ToString(),
                               PhoneNumber = e.PhoneNumber,
                               Position = e.Position.ToString(),
                               StructureName = e.OrganizationalStructure.StructureName,
                               BranchId = e.OrganizationalStructure.OrganizationBranch.Name,
                               StructureId = e.OrganizationalStructureId.ToString(),
                               Remark = e.Remark

                           }).FirstOrDefaultAsync();

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
         

            orgEmployee.Photo = employeeDto.Photo;
            orgEmployee.Title = employeeDto.Title;
            orgEmployee.FullName = employeeDto.FullName;
            orgEmployee.Gender = Enum.Parse<Gender>( employeeDto.Gender);
            orgEmployee.PhoneNumber = employeeDto.PhoneNumber;
            orgEmployee.Remark = employeeDto.Remark;
            orgEmployee.Position = Enum.Parse<Position>( employeeDto.Position);
            orgEmployee.OrganizationalStructureId = Guid.Parse(employeeDto.StructureId);
            orgEmployee.RowStatus = employeeDto.RowStatus==0?RowStatus.Active:RowStatus.InActive;


            _dBContext.Entry(orgEmployee).State = EntityState.Modified;
            await _dBContext.SaveChangesAsync();
            return 1;

        }

        public async Task<List<SelectListDto>> GetEmployeeByStrucutreSelectList(Guid StructureId)
        {


            List<SelectListDto> employees = await (from e in _dBContext.Employees.Where(x => x.OrganizationalStructureId == StructureId)
                                                  
                                   select new SelectListDto
                                   {
                                       Id = e.Id,
                                       Name =$"{e.FullName} ( {e.Position } ) "
                                   }).ToListAsync();
            return employees;


        }

    }
}

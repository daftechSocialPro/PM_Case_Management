using Microsoft.EntityFrameworkCore;
using PM_Case_Managemnt_API.Data;
using PM_Case_Managemnt_API.DTOS.Common;
using PM_Case_Managemnt_API.Models.Common;


namespace PM_Case_Managemnt_API.Services.Common
{
    public class EmployeeService : IEmployeeService
    {

        private readonly DBContext _dBContext;
        public EmployeeService(DBContext context)
        {
            _dBContext = context;
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

        public async Task<List<EmployeeDto>> GetEmployees()
        {



           



            var k=  await (from e in _dBContext.Employees
                          join es in _dBContext.EmployeesStructures.Include(x => x.OrganizationalStructure) on e.Id equals es.EmployeeId

                          select new EmployeeDto
                          {
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

       








        public async Task<int> UpdateEmployee(EmployeeDto organizationProfile)
        {


            //var orgBranch = _dBContext.OrganizationBranches.Where(x => x.IsHeadOffice).FirstOrDefault();

            //orgBranch.OrganizationProfileId = organizationProfile.Id;
            //orgBranch.Name = organizationProfile.OrganizationNameEnglish;
            //orgBranch.Address = organizationProfile.Address;
            //orgBranch.PhoneNumber = organizationProfile.PhoneNumber;
            //orgBranch.IsHeadOffice = true;
            //orgBranch.Remark = organizationProfile.Remark;



            //_dBContext.Entry(orgBranch).State = EntityState.Modified;
            //await _dBContext.SaveChangesAsync();

            //_dBContext.Entry(organizationProfile).State = EntityState.Modified;
            //await _dBContext.SaveChangesAsync();
            return 1;

        }

    }
}

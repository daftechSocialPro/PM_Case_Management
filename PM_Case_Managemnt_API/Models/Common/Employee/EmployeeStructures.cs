

namespace PM_Case_Managemnt_API.Models.Common
{
    public class EmployeeStructures:CommonModel
    {
        public virtual  Employee Employee { get; set; }

        public Guid EmployeeId { get; set; }


        public virtual OrganizationalStructure OrganizationalStructure { get; set; }

        public Guid OrganizationalStructureId { get; set; }


        public Position Position { get; set; }

    }
}

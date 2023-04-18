namespace PM_Case_Managemnt_API.Models.Common
{
    public class OrganizationalStructure :CommonModel
    {

        public OrganizationalStructure()
        {

          //  EmployeesCollection = new HashSet<Employee>();
            SubTask = new HashSet<OrganizationalStructure>();
        }
        public virtual OrganizationBranch OrganizationBranch { get; set; } = null!;

        public Guid OrganizationBranchId { get; set; }


        public Guid? ParentStructureId { get; set; }
        public virtual OrganizationalStructure ParentStructure { get; set; } = null!;

        public string StructureName { get; set; } = null!;

        public int Order { get; set; }

        public float Weight { get; set; }
        public ICollection<OrganizationalStructure> SubTask { get; set; }

    }
}

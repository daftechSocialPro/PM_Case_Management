namespace PM_Case_Managemnt_API.Models.Common
{
    public class OrganizationalStructure :CommonModel
    {
        public virtual OrganizationBranch OrganizationBranch { get; set; }

        public Guid OrganizationBranchId { get; set; }


        public Guid? ParentStructureId { get; set; }
        public virtual OrganizationalStructure ParentStructure { get; set; }

        public string StructureName { get; set; }

        public int Order { get; set; }

        public float Weight { get; set; }


    }
}

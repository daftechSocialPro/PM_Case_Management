
namespace PM_Case_Managemnt_API.DTOS.Common
{
    public class OrgStructureDto
    {
        public Guid? Id { get; set; }
        public Guid OrganizationBranchId { get; set; }
        public string? BranchName { get; set; }

        public Guid? ParentStructureId { get; set; }
        public string? ParentStructureName { get; set; }
        public string StructureName { get; set; }
        public float? ParentWeight { get; set; }

        public int Order { get; set; }

        public float Weight { get; set; }

        public string Remark { get; set; }

        public int RowStatus { get; set; }
    }
}

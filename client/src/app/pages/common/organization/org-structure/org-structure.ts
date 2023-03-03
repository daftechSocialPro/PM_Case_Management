export interface OrganizationalStructure {
  Id: string;
  OrganizationBranchId: string;
  BranchName: string;
  ParentStructureId: string;
  ParentStructureName: string;
  StructureName: string;
  Order: Number;
  Weight: Number;
  ParentWeight?: Number;
  Remark: string;
  RowStatus: Number;
}

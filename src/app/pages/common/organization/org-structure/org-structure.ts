export interface OrganizationalStructure {

    Id:string,
    OrganizationBranchId: string;
    BranchName: string;
    ParentStructureId: string;
    ParentStructureName: string;
    StructureName: String;
    Order: Number;
    Weight: Number;
    Remark: string;
    RowStatus:Number;

}
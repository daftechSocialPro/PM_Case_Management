export interface Program {

    ProgramName: string ;     
    ProgramPlannedBudget:Number;
    ProgramBudgetYear:string ;
    ProgramStructures :ProgramStructure[];
    NumberOfProjects: Number;
    Remark:string;
    
}

export interface ProgramStructure {

    StructureName : string ; 
    StructureHead : string ; 
}
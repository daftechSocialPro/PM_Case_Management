export interface Plan {
    BudgetYearId: String
    HasTask: Boolean
    PlanName: String
    PlanWeight: Number
    PlandBudget: Number
    ProgramId: String
    ProjectType: String
    Remark: String
    StructureId: String;
    ProjectManagerId: String;
    FinanceId: String;

}


export interface PlanView {

    PlanName: String,
    PlanWeight: Number,
    PlandBudget: Number,
    RemainingBudget: Number,
    ProjectManager: String,
    FinanceManager: string,
    Director: string,
    StructureName: String,
    ProjectType: String,
    NumberOfTask: String,
    NumberOfActivities: String,
    NumberOfTaskCompleted: String



}





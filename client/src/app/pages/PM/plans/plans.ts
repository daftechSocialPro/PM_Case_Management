import { TaskView } from "../tasks/task"

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

    Id : string,
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
    NumberOfTaskCompleted: String,
    HasTask:Number,



}

export interface PlanSingleview {
    Id:String,
    PlanName:String,
    PlanWeight:String,
    RemainingWeight:String,
    PlannedBudget:String,
    RemainingBudget:String,
    StartDate:Date,
    EndDate:Date
    Tasks :TaskView[]

}



  




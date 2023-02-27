import { SelectList } from "../../common/common"
import { ActivityView } from "../view-activties/activityview"

export interface Task {

    TaskDescription: String,
    HasActvity: Boolean,
    PlannedBudget: Number,
    PlanId: String,

}

export interface TaskView {

    Id?: String
    TaskName?: String
    TaskWeight?: Number
    NumberofActivities?: Number
    FinishedActivitiesNo?: Number
    TerminatedActivitiesNo?: Number
    StartDate?: Date
    EndDate?: Date
    NumberOfMembers?: Number
    HasActivity?: Boolean
    PlannedBudget?: Number
    TaskMembers?: SelectList[]
    TaskMemos?:TaskMemoView[]
    ActivityViewDtos ?:ActivityView[]

}
export interface TaskMembers {


    Employee: SelectList[]
    TaskId: String

}


export interface TaskMemoView{
    Employee : SelectList
    Description: String 
    DateTime:string
}
export interface TaskMemo{
    EmployeeId:String,
    Description:String,
    TaskId :String
}
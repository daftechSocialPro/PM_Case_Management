import { SelectList } from "../../common/common"

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

}
export interface TaskMembers {


    Employee: SelectList[]
    TaskId: String

}
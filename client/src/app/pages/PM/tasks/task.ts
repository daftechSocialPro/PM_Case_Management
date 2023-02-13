export interface Task {
  
   TaskDescription:String ,
   HasActivity : Boolean,
   PlannedBudget:Number ,
   PlanId : String,   

}

export interface TaskView {

    Id: String 
    TaskName : String  
    TaskWeight : Number
    NumberofActivities: Number
    FinishedActivitiesNo : Number
    TerminatedActivitiesNo : Number
    StartDate :Date
    EndDate :Date
    NumberOfMembers : Number
    HasActivity:Boolean
    PlannedBudget: Number

}

import { SelectList } from "../../common/common";

export interface ActivityView {

    Id: string
    Name : string ; 
    PlannedBudget: number ; 
    ActivityType : string ; 
    Weight : Number 
    Beginning : Number
    Target : Number
    UnitMeasurment : string,
    OverAllPerformance :Number,
    StartDate : string ,
    EndDate : string,
    Members : SelectList[]
    MonthPerformance ?: MonthPerformanceView[]


}
export interface MonthPerformanceView {

    MonthName : string ,
    Planned : Number,
    Actual: Number,
    Percentage : Number 
}
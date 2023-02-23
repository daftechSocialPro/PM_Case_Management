import { SelectList } from "../../common/common";

export interface ActivityView {

    Id: string
    Name : string ; 
    PlannedBudget: number ; 
    ActivityType : string ; 
    Weight : Number 
    Begining : number
    Target : number
    UnitOfMeasurment : string,
    OverAllPerformance :Number,
    StartDate : string ,
    EndDate : string,
    Members : SelectList[]
    MonthPerformance ?: MonthPerformanceView[]


}
export interface MonthPerformanceView {
    order:number,
    MonthName : string ,
    Planned : Number,
    Actual: Number,
    Percentage : Number 
}


export interface ActivityTargetDivisionDto {

    ActiviyId:string;
    CreatedBy:string;
    TargetDivisionDtos:TargetDivisionDto[]
}
export interface TargetDivisionDto {

    Order:number,
    Target: number,
    TargetBudget:number
}

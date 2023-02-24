import { SelectList } from "../../common/common";

export interface ActivityView {

    Id: string
    Name: string;
    PlannedBudget: number;
    ActivityType: string;
    Weight: Number
    Begining: number
    Target: number
    UnitOfMeasurment: string,
    OverAllPerformance: Number,
    StartDate: string,
    EndDate: string,
    Members: SelectList[]
    MonthPerformance?: MonthPerformanceView[]


}
export interface MonthPerformanceView {

    Id: string;
    Order: number,
    MonthName: string,
    Planned: Number,
    Actual: Number,
    Percentage: Number
}


export interface ActivityTargetDivisionDto {

    ActiviyId: string;
    CreatedBy: string;
    TargetDivisionDtos: TargetDivisionDto[]
}
export interface TargetDivisionDto {

    Order: number,
    Target: number,
    TargetBudget: number
}


export interface AddProgressActivityDto {

    ActivityId: string,
    QuarterId: string,
    EmployeeValueId: string,
    ProgressStatus: number,
    ActualBudget: number,
    ActualWorked: number,
    Lat: string,
    lng: string,
    CreatedBy: string,
    Remark: string
}

export interface ViewProgressDto {
    Id: string
    ActalWorked: number
    UsedBudget: number
    Documents: string[]
    FinanceDocument ?:string 
    Remark: string
    IsApprovedByManager: string
    IsApprovedByFinance: string
    IsApprovedByDirector: string
    FinanceApprovalRemark: string
    CoordinatorApprovalRemark: string
    DirectorApprovalRemark: string
    CreatedAt:string


}



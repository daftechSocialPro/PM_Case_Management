export interface ICaseReport {

    Id: string
    CaseNumber: string
    CaseType: string
    Subject: string
    IsArchived: string
    OnStructure: string
    OnEmployee: string
    CaseStatus: string
    CreatedDateTime: Date
    CaseCounter: number
    ElapsTime: number
}

export interface ICaseReportChart {
    Name : string ; 
    Value : number ; 
}
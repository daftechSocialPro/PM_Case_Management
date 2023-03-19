export interface IDashboardDto{

    pendingReports : IDashboardView[]
    completedReports :IDashboardView[]
}

export interface IDashboardView{

 ApplicantName :string
 AffairNumber :string
 Subject :string
 Structure :string
 Employee :string
 Elapstime :number
 Level :number
 CreatedDateTime :string

}


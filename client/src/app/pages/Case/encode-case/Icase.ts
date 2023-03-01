import { SelectList } from "../../common/common"

export interface ICaseView{

    Id : string,
    CaseNumber : string,
    LetterNumber : string,
    LetterSubject : string,
    CaseTypeName : string,
    ApplicantName : string,
    EmployeeName : string,
    ApplicantPhoneNo : string,
    EmployeePhoneNo : string
    CreatedAt : string,
    FromStructure :string,
    FromEmployeeId :string,
    ReciverType :string,
    IsSMSSent:boolean,
    ToStructure :string,
    ToEmployee :string,
    ToEmployeeId :string,
    Position:string,
    SecreateryNeeded:boolean,
    IsConfirmedBySeretery :boolean,
    AffairHistoryStatus :string
    Attachments:SelectList[]
  

} 
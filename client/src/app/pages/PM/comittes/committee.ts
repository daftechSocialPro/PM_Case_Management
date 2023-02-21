import { SelectList } from "../../common/common";

export interface CommitteeView {

    Id : string ; 
    Name : string ; 
    NoOfEmployees: Number ;
    EmployeeList : SelectList[];
}

export interface ComiteeAdd {

    Name : string ; 
    Remark : string ;
    CreatedBy : string ;
  
}

export interface CommiteeAddEmployeeView 
{
    CommiteeId:String;
    EmployeeList:string[];
    CreatedBy:string;
}


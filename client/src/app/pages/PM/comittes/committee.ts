import { SelectList } from "../../common/common";

export interface CommitteeView {

    Id : String ; 
    CommitteeName : string ; 
    NoOfEmployee: Number ;
    Employees : SelectList[];
}
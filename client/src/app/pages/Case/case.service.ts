import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SelectList } from '../common/common';
import { CaseType, CaseTypeView, FileSettingView } from './case-type/casetype';
import { ICaseView } from './encode-case/Icase';


@Injectable({
    providedIn: 'root',
})
export class CaseService {
    constructor(private http: HttpClient) { }
    BaseURI: string = environment.baseUrl + "/case"
    //caseType
    createCaseType(casetype: CaseType) {

        return this.http.post(this.BaseURI + "/type", casetype)
    }
    getCaseType() {
        return this.http.get<CaseTypeView[]>(this.BaseURI + "/type")
    }

    getSelectCasetType() {
        return this.http.get<SelectList[]>(this.BaseURI + "/typeSelectList")
    }

    getCaseTypeByCaseForm(caseForm: string) {

        return this.http.get<SelectList[]>(this.BaseURI + "/byCaseForm?caseForm=" + caseForm)
    }

    //file setting 

    createFileSetting(fileSetting: any) {
        return this.http.post(this.BaseURI + "/fileSetting", fileSetting)
    }
    getFileSetting() {
        return this.http.get<FileSettingView[]>(this.BaseURI + "/fileSetting")
    }

    getFileSettignsByCaseTypeId(caseTypeId: string) {

        return this.http.get<SelectList[]>(this.BaseURI + "/fileSettingsByCaseTypeId?CaseTypeId=" + caseTypeId)
    }

    //applicant 

    createApplicant(applicant: any) {
        return this.http.post(this.BaseURI + "/applicant", applicant)
    }

    getApplicantSelectList() {

        return this.http.get<SelectList[]>(this.BaseURI + "/applicantSelectList")
    }

    //case
    addCase(caseValue: FormData) {


        return this.http.post(this.BaseURI + "/encoding", caseValue)
    }
    getEncodedCases(userId: string) {
        return this.http.get<ICaseView[]>(this.BaseURI + "/encoding?userId=" + userId)

    }
    getMyCaseList (employeeId: string ){

        return this.http.get<ICaseView[]>(this.BaseURI+"/mycaseList?employeeId="+employeeId)
    }
    //notification 
    getCasesNotification(employeeId: string) {


        return this.http.get<ICaseView[]>(this.BaseURI + "/getnotification?employeeId=" + employeeId)


    }



    getCaseNumber() {
        var HTTPOptions = {
            headers: new HttpHeaders({
                'Accept': 'text'
            }),
            'responseType': 'text' as 'json'
        }

        return this.http.get<string>(this.BaseURI + "/getCaseNumber", HTTPOptions)
    }


    //assign case

    assignCase(assigncase: any) {
        return this.http.post(this.BaseURI + "/assign", assigncase)
    }




    //casetransaction 

    ConfirmTransaction (confirmtracactionDto : any){
        return this.http.put(this.BaseURI+"/confirmcase",confirmtracactionDto)
   
    }
    GetCaseHistories (EmployeeId : string ,CaseHistoryId:string){
        

     return this.http.get<ICaseView[]>(this.BaseURI+"/getHistories?EmployeeId="+EmployeeId+"&CaseHistoryId="+CaseHistoryId)
    }

    GetCaseDetail (EmployeeId : string ,CaseHistoryId:string){

        return this.http.get<ICaseView>(this.BaseURI+"/getCaseDetail?EmployeeId="+EmployeeId+"&CaseHistoryId="+CaseHistoryId)
        
    }
}




import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SelectList } from '../common/common';
import { IAppointmentGet, IAppointmentWithCalander } from './case-detail/make-appointment-case/Iappointmentwithcalander';
import { CaseType, CaseTypeView, FileSettingView } from './case-type/casetype';
import { ICaseView } from './encode-case/Icase';
import { IUnsentMessage } from './list-of-messages/Imessage';


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

    getOrderNumber(caseTypeId:string){

        return this.http.get<number>(this.BaseURI+"/GetChildOrder?caseTypeId="+caseTypeId)
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
        return this.http.put(this.BaseURI+"/confirm",confirmtracactionDto)
   
    }
    GetCaseHistories (EmployeeId : string ,CaseHistoryId:string){
        

     return this.http.get<ICaseView[]>(this.BaseURI+"/getHistories?EmployeeId="+EmployeeId+"&CaseHistoryId="+CaseHistoryId)
    }

    GetCaseDetail (EmployeeId : string ,CaseHistoryId:string){

        return this.http.get<ICaseView>(this.BaseURI+"/getCaseDetail?EmployeeId="+EmployeeId+"&CaseHistoryId="+CaseHistoryId)
        
    }

    //actions
    AddtoWaiting(caseHistoryId:string){

        return this.http.post(this.BaseURI+"/waiting?caseHistoryId="+caseHistoryId,{})
    }
    CompleteCase(completecasedto : any ){

        return this.http.post (this.BaseURI+"/complete",completecasedto)
    }
    RevertCase (reveertcasedto:any){

        return this.http.post (this.BaseURI+"/revert",reveertcasedto)
    }
        
    SendSms (smscasedto:any){

        return this.http.post(this.BaseURI+"/sendSms",smscasedto)
    }
    TransferCase(transferCaseDto:FormData){
        
        return this.http.post(this.BaseURI+"/transfer",transferCaseDto)
    }
    AppointCase (appointment: IAppointmentWithCalander){

        return this.http.post<IAppointmentGet>(this.BaseURI+"/appointmetWithCalender",appointment)
    }

    //
    getAppointment (employeeId:string){

        return this.http.get<IAppointmentGet[]>(this.BaseURI+"/appointmetWithCalender?employeeId="+employeeId)
    }

    getMessages(){

        return this.http.get<IUnsentMessage[]>(this.BaseURI+"/CaseMessages")
    }
        
    //get completed casses to archive 

    getCompletedCases (){

        return this.http.get<ICaseView[]>(this.BaseURI+"/completedList")
    }
    
    archiveCase(archive:any){

        return this.http.post(this.BaseURI+"/archive",archive)
    }

    getArchiveCases (){

        return this.http.get<ICaseView[]>(this.BaseURI+"/getArchivedCases")
    }
        
}




import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SelectList } from '../common/common';
import { CaseType, CaseTypeView, FileSettingView } from './case-type/casetype';


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

    getCaseTypeByCaseForm(caseForm: string ){

        return this.http.get<SelectList[]>(this.BaseURI+"/byCaseForm?caseForm="+caseForm)
    }

    //file setting 

    createFileSetting(fileSetting: any) {
        return this.http.post(this.BaseURI + "/fileSetting", fileSetting)
    }
    getFileSetting() {
        return this.http.get<FileSettingView[]>(this.BaseURI + "/fileSetting")
    }

    //applicant 

    createApplicant (applicant : any ){
        return this.http.post(this.BaseURI+"/applicant",applicant)
    }

    getApplicantSelectList (){

        return this.http.get<SelectList[]>(this.BaseURI+"/applicantSelectList")
    }
    addCase(caseValue:any){

        return this.http.post(this.BaseURI,caseValue)
    }

    getCaseNumber (){
        return this.http.get<string>(this.BaseURI+"/getCaseNumebr")
    }
}





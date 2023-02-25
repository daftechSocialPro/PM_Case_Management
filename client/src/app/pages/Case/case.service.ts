import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CaseType, CaseTypeView } from './case-type/casetype';


@Injectable({
    providedIn: 'root',
})
export class CaseService {
    constructor(private http: HttpClient) { }
    BaseURI: string = environment.baseUrl + "/case"

createCaseType (casetype : CaseType){

    return this.http.post(this.BaseURI+"/type",casetype)
}
getCaseType (){
    return this.http.get<CaseTypeView[]>(this.BaseURI+"/type")
}



}





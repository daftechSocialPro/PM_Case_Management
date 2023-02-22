import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';



@Injectable({
    providedIn: 'root',
})
export class CaseService {
    constructor(private http : HttpClient) { }
    BaseURI: string = environment.baseUrl + "/Case"

    //case

    addCase (caseParm : any ){

        return this.http.post(this.BaseURI,caseParm)
    }





}
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';


@Injectable({
    providedIn: 'root',
})
export class PlanService {
    constructor(private http: HttpClient) { }
    BaseURI: string = environment.baseUrl + "/PM/Plan"


    //Plan 

    // createProgram(program: Program) {
    //     return this.http.post(this.BaseURI, program)
    // }

    // getPrograms (){
    //     return this.http.get<Program[]>(this.BaseURI)
    // }



}
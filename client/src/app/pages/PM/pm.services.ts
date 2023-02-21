import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { SelectList } from '../common/common';
import { ComiteeAdd, CommiteeAddEmployeeView, CommitteeView } from './comittes/committee';


@Injectable({
    providedIn: 'root',
})
export class PMService {
    constructor(private http: HttpClient) { }
    BaseURI: string = environment.baseUrl + "/PM"

    //comittee

    createComittee(ComiteeAdd: ComiteeAdd) {

        return this.http.post(this.BaseURI + "/Commite", ComiteeAdd)
    }

    getComittee() {
        return this.http.get<CommitteeView[]>(this.BaseURI + "/Commite")
    }

    getNotIncludedEmployees(CommiteId:string) {

        return this.http.get<SelectList[]>(this.BaseURI + "/Commite/getNotIncludedEmployees?CommiteId="+CommiteId)
    }

    addEmployesInCommitee(value: CommiteeAddEmployeeView) {
        return this.http.post(this.BaseURI + "/Commite/addEmployesInCommitee", value)
    }
    removeEmployesInCommitee(value: CommiteeAddEmployeeView) {
        return this.http.post(this.BaseURI + "/Commite/removeEmployesInCommitee", value)
    }


}
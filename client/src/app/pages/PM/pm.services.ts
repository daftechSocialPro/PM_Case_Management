import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { SelectList } from '../common/common';
import { ActivityDetailDto } from './activity-parents/add-activities/add-activities';
import { ComiteeAdd, CommiteeAddEmployeeView, CommitteeView } from './comittes/committee';
import { ActivityTargetDivisionDto, ActivityView, ViewProgressDto } from './view-activties/activityview';


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
    updateComittee(comiteeAdd: ComiteeAdd) {

        return this.http.put(this.BaseURI + "/Commite", comiteeAdd)
    }

    getComittee() {
        return this.http.get<CommitteeView[]>(this.BaseURI + "/Commite")
    }

    getComitteeSelectList() {

        return this.http.get<SelectList[]>(this.BaseURI + "/Commite/getSelectListCommittee")
    }

    getNotIncludedEmployees(CommiteId: string) {

        return this.http.get<SelectList[]>(this.BaseURI + "/Commite/getNotIncludedEmployees?CommiteId=" + CommiteId)
    }

    addEmployesInCommitee(value: CommiteeAddEmployeeView) {
        return this.http.post(this.BaseURI + "/Commite/addEmployesInCommitee", value)
    }
    removeEmployesInCommitee(value: CommiteeAddEmployeeView) {
        return this.http.post(this.BaseURI + "/Commite/removeEmployesInCommitee", value)
    }

    /// Activity Parent 

    addActivityParent(activity: ActivityDetailDto) {
        return this.http.post(this.BaseURI + "/Activity", activity)
    }
    addActivityTargetDivision(activityDto: ActivityTargetDivisionDto) {

        return this.http.post(this.BaseURI + "/Activity/targetDivision", activityDto)

    }

    addActivityPorgress(progress: FormData) {

        return this.http.post(this.BaseURI + "/Activity/addProgress", progress)
    }
    viewProgress (activityId : string){

        return this.http.get<ViewProgressDto[]>(this.BaseURI+"/Activity/viewProgress?actId="+activityId)
    }

    getAssignedActivities (empId : string ){

        return this.http.get<ActivityView[]>(this.BaseURI+"/Activity/getAssignedActivties?employeeId="+empId)
    }



}
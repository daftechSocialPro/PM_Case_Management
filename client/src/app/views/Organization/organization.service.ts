import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, } from "@angular/common/http";
import { OrganizationProfile } from './organization-profile/organization-profile'
import { OrganizationBranch } from './organization-branches/organization-branch'
import { SelectList } from '../../shared/model/select-list';
import {OrganizationalStructure} from './organization-structures/OrganizationalStructure'
@Injectable({
  providedIn: 'root'
})
export class OrganizationService {

  constructor(private http: HttpClient) { }
  readonly BaseURI = environment.baseUrl;

  //organization
  OrganizationCreate(formData: FormData) {
    return this.http.post(environment.baseUrl + "/Organization", formData, { reportProgress: true, observe: 'events' })

  }
  OrganizationUpdate(formData: FormData) {
    return this.http.put(environment.baseUrl + "/Organization", formData, { reportProgress: true, observe: 'events' })

  }
  getOrganizationalProfile() {
    return this.http.get<OrganizationProfile>(environment.baseUrl + "/Organization")
  }

  // branch
  OrgBranchCreate(orgBranch: OrganizationBranch) {
    return this.http.post(environment.baseUrl + "/OrgBranch", orgBranch)
  }

  getOrgBranches() {
    return this.http.get<OrganizationBranch[]>(environment.baseUrl + "/OrgBranch")
  }

  getOrgBranchSelectList() {
    return this.http.get<SelectList[]>(environment.baseUrl + "/OrgBranch/branchlist")
  }


  //OrgStructure

  OrgStructureCreate(OrgStructure: OrganizationalStructure) {
    return this.http.post(environment.baseUrl + "/OrgStructure", OrgStructure)
  }

  getOrgStructureList() {
    return this.http.get<OrganizationalStructure[]>(environment.baseUrl + "/OrgStructure")
  }

  getOrgStructureSelectList(){
    return this.http.get<SelectList[]>(environment.baseUrl+"/OrgStructure/parentStructures")
  }


}

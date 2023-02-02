import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SelectList } from '../common';
import { UnitMeasurment } from '../unit-measurement/unit-measurment';
import { Employee } from './employee/employee';
import { OrganizationBranch } from './org-branch/org-branch';
import { OrganizationProfile } from './org-profile/org-profile';
import { OrganizationalStructure } from './org-structure/org-structure';

@Injectable({
  providedIn: 'root'
})
export class OrganizationService {

  constructor(private http: HttpClient) { }
  readonly BaseURI = environment.baseUrl;

  //organization
  OrganizationCreate(formData: FormData) {
    return this.http.post(this.BaseURI + "/Organization", formData, { reportProgress: true, observe: 'events' })

  }
  OrganizationUpdate(formData: FormData) {
    return this.http.put(this.BaseURI + "/Organization", formData, { reportProgress: true, observe: 'events' })

  }
  getOrganizationalProfile() {
    return this.http.get<OrganizationProfile>(this.BaseURI + "/Organization")
  }

  // branch
  OrgBranchCreate(orgBranch: OrganizationBranch) {
    return this.http.post(this.BaseURI + "/OrgBranch", orgBranch)
  }
  orgBranchUpdate(orgBranch: OrganizationBranch) {
    return this.http.put(this.BaseURI + "/OrgBranch", orgBranch)
  }

  getOrgBranches() {
    return this.http.get<OrganizationBranch[]>(this.BaseURI + "/OrgBranch")
  }

  getOrgBranchSelectList() {
    return this.http.get<SelectList[]>(this.BaseURI + "/OrgBranch/branchlist")
  }


  //OrgStructure

  OrgStructureCreate(OrgStructure: OrganizationalStructure) {
    return this.http.post(this.BaseURI + "/OrgStructure", OrgStructure)
  }

  getOrgStructureList() {
    return this.http.get<OrganizationalStructure[]>(this.BaseURI + "/OrgStructure")
  }

  getOrgStructureSelectList(branchid: string) {
    return this.http.get<SelectList[]>(this.BaseURI + "/OrgStructure/parentStructures?branchid=" + branchid)
  }

  // employee

  employeeCreate(employee: FormData) {

    return this.http.post(this.BaseURI + "/Employee", employee);

  }
  employeeUpdate(formData: FormData) {
    return this.http.put(this.BaseURI + "/Employee", formData)

  }
  getEmployees() {
    return this.http.get<Employee[]>(this.BaseURI + "/Employee");
  }

  //unit of measurment 

  unitOfMeasurmentCreate(unitmeasurment: UnitMeasurment) {
    return this.http.post(this.BaseURI + "/UnitOfMeasurment", unitmeasurment)
  }

  getUnitOfMeasurment() {
    return this.http.get<UnitMeasurment[]>(this.BaseURI + "/UnitOfMeasurment")
  }

  getUnitOfMeasurmentSelectList() {
    return this.http.get<SelectList[]>(this.BaseURI + "/UnitOfMeasurment/unitmeasurmentlist")
  }



}

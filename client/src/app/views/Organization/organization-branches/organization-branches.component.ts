import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { OrganizationService } from '../organization.service';
import { CommonService, toastPayload } from 'src/app/shared/common.service';
import { IndividualConfig } from 'ngx-toastr';
import { OrganizationBranch } from './organization-branch';

@Component({
  selector: 'app-organization-branches',
  templateUrl: './organization-branches.component.html',
  styleUrls: ['./organization-branches.component.scss']
})
export class OrganizationBranchesComponent implements OnInit {
 

  dtOptions: DataTables.Settings = {};

  constructor( private orgService: OrganizationService) {
   
  }

  ngOnInit(): void {

    this.branchList()
 
  }

  branchList (){

    this.dtOptions = {
      destroy:true,
      ajax: ({ }, callback) => {
        this.orgService.getOrgBranches().subscribe(resp => {
          callback({
            data: resp
          });
        });
      },
      columns: [
        {
          title: 'OragnizationalName',
          data: 'OrganizationProfile.OrganizationNameEnglish'
        },
        {
          title: 'Name',
          data: 'Name'
        }, {
          title: 'Address',
          data: 'Address'
        },
        {
          title: 'PhoneNumber',
          data: 'PhoneNumber'
        },
        {
          title: 'Remark',
          data: 'Remark'
        }]}
  }
  public createVisible = false;

  toggleCreateBranch() {
    this.createVisible = !this.createVisible;
  }


  handleCreateBranchChange(event: any) {
    this.createVisible = event;
  }


}

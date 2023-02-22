import { Component, ElementRef, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { OrganizationService } from '../organization.service';
import { AddBranchComponent } from './add-branch/add-branch.component';
import { OrganizationBranch } from './org-branch';
import { UpdateBranchComponent } from './update-branch/update-branch.component';

@Component({
  selector: 'app-org-branch',
  templateUrl: './org-branch.component.html',
  styleUrls: ['./org-branch.component.css']
})
export class OrgBranchComponent implements OnInit {

  branches: OrganizationBranch[] = [];
  toast!: toastPayload;
  branch!: OrganizationBranch;

  constructor(private elementRef: ElementRef, private orgService: OrganizationService, private commonService: CommonService, private modalService: NgbModal) {
    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "../assets/js/main.js";
    this.elementRef.nativeElement.appendChild(s);
  }

  ngOnInit(): void {
    this.branchList()
  }

  branchList() {
    this.orgService.getOrgBranches().subscribe({
      next: (res) => {
        this.branches = res


      }, error: (err) => {
        this.toast = {
          message: 'Something went wrong',
          title: 'Network error.',
          type: 'error',
          ic: {
            timeOut: 2500,
            closeButton: true,
          } as IndividualConfig,
        };
        this.commonService.showToast(this.toast);
      }
    })
  }

  addBranch() {

    let modalRef = this.modalService.open(AddBranchComponent, {
      size: 'lg',
      backdrop: 'static'
    })

    modalRef.result.then((result)=>{
   
        this.branchList;
      
      
    })



  }

  Update(vlaue: any) {

    let modalref = this.modalService.open(UpdateBranchComponent, {
      size: 'lg',
      backdrop: 'static'
    })
    modalref.componentInstance.branch = vlaue
    modalref.result.then((isresult) => {

      this.branchList()

    })


  }

}

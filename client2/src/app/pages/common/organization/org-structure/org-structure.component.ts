import { Component, ElementRef, OnInit } from '@angular/core';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { OrganizationService } from '../organization.service';
import { OrganizationalStructure } from './org-structure';

@Component({
  selector: 'app-org-structure',
  templateUrl: './org-structure.component.html',
  styleUrls: ['./org-structure.component.css']
})
export class OrgStructureComponent implements OnInit {

  structures: OrganizationalStructure[] = [];
  toast!: toastPayload;
  structure!: OrganizationalStructure;
  
  constructor(private elementRef:ElementRef, private orgService : OrganizationService, private commonService:CommonService) { 
    this.structureList()
  }

  ngOnInit(): void {

    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "../assets/js/main.js";
    this.elementRef.nativeElement.appendChild(s);
    this.structureList()
   
  }
  structureList() {

    this.orgService.getOrgStructureList().subscribe({
      next: (res) => {
        this.structures = res
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

}

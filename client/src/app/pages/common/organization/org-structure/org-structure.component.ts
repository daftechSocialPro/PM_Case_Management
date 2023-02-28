import { Component, ElementRef, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { OrganizationService } from '../organization.service';
import { AddStructureComponent } from './add-structure/add-structure.component';
import { OrganizationalStructure } from './org-structure';
import { UpdateStructureComponent } from './update-structure/update-structure.component';

declare const jsc: any;

@Component({
  selector: 'app-org-structure',
  templateUrl: './org-structure.component.html',
  styleUrls: ['./org-structure.component.css'],
})
export class OrgStructureComponent implements OnInit {
  structures: OrganizationalStructure[] = [];
  toast!: toastPayload;
  structure!: OrganizationalStructure;

  constructor(
    private elementRef: ElementRef,
    private orgService: OrganizationService,
    private commonService: CommonService,
    private modalService: NgbModal
  ) {
    this.structureList();
  }

  ngOnInit(): void {
    var s = document.createElement('script');
    s.type = 'text/javascript';
    s.src = '../assets/js/main.js';
    this.elementRef.nativeElement.appendChild(s);
    this.structureList();
  }
  structureList() {
    this.orgService.getOrgStructureList().subscribe({
      next: (res) => {
        this.structures = res;
      },
      error: (err) => {
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
      },
    });
  }

  addStructure() {
    let modalRef = this.modalService.open(AddStructureComponent, {
      size: 'lg',
      backdrop: 'static',
    });
    modalRef.result.then(() => {
      this.structureList();
    });
  }
  updateStructure(value: OrganizationalStructure) {
    let modalRef = this.modalService.open(UpdateStructureComponent, {
      size: 'lg',
      backdrop: 'static',
    });

    modalRef.componentInstance.structure = value;

    modalRef.result.then(() => {
      this.structureList();
    });
  }
}

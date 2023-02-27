import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { SelectList } from 'src/app/pages/common/common';
import { OrganizationService } from 'src/app/pages/common/organization/organization.service';
import { CaseService } from '../../case.service';

@Component({
  selector: 'app-assign-case',
  templateUrl: './assign-case.component.html',
  styleUrls: ['./assign-case.component.css']
})

export class AssignCaseComponent implements OnInit {

  @Input() caseId!: string
  caseForm !: FormGroup;
  Branches !: SelectList[]
  Structures !: SelectList[]
  Employees !: SelectList[]
  toast!: toastPayload


  constructor(
    private activeModal: NgbActiveModal,
    private organizationService: OrganizationService,
    private formBuilder: FormBuilder,
    private caseService: CaseService,
    private commonService: CommonService) {

    this.caseForm = this.formBuilder.group({
      selectwho: [0, Validators.required],
      Structure: ['', Validators.required],
      ForEmployee: ['', Validators.required],
      CCto: ['', Validators.required]
    })



  }
  ngOnInit(): void {

    this.getBranches()

  }
  getBranches() {
    this.organizationService.getOrgBranchSelectList().subscribe({
      next: (res) => {
        this.Branches = res
      }, error: (err) => {
        console.error(err)
      }
    })
  }
  getStructures(branchId: string) {

    this.organizationService.getOrgStructureSelectList(branchId).subscribe({
      next: (res) => {
        this.Structures = res
      }, error: (err) => {
        console.error(err)
      }
    })
  }
  getEmployees(structureId: string) {

    this.organizationService.getEmployeesBystructureId(structureId).subscribe({
      next: (res) => {
        this.Employees = res
      }, error: (err) => {
        console.error(err)
      }
    })
  }




  submit() {

    if (this.caseForm.valid) {

      this.caseService.assignCase(
        {
          "CaseId": "275C1D3A-707F-4179-9E67-90FF1CF1FC86",
          "AssignedByEmployeeId": "278B4187-413D-4F28-A63A-3D4F2B6C7F45",
          "AssignedToEmployeeId": "4390EAD4-EEA7-408C-8E7C-2FDB2923258E",
          "ForwardedToStructureId": [
            "973514F9-E636-4564-8B2F-3B501495698A"
          ]
        }
      ).subscribe({
        next: (res) => {
          this.toast = {
            message: ' Case Assigned Successfully',
            title: 'Successfully Created.',
            type: 'success',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);
          this.closeModal();
        }, error: (err) => {

          this.toast = {
            message: 'Something went wrong',
            title: 'Network Error.',
            type: 'error',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);
          this.closeModal();

        }
      })
    }
    else {

    }

  }

  closeModal() {

    this.activeModal.close()
  }

}

import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { toastPayload, CommonService } from 'src/app/common/common.service';
import { SelectList } from 'src/app/pages/common/common';
import { OrganizationService } from 'src/app/pages/common/organization/organization.service';
import { UserView } from 'src/app/pages/pages-login/user';
import { UserService } from 'src/app/pages/pages-login/user.service';
import { CaseService } from '../../case.service';
import { ICaseState } from './IcaseState';

@Component({
  selector: 'app-transfer-case',
  templateUrl: './transfer-case.component.html',
  styleUrls: ['./transfer-case.component.css']
})
export class TransferCaseComponent implements OnInit {

  @Input() historyId!: string
  @Input() CaseTypeName!:string
  @Input() CaseTypeId ! : string
  user!: UserView
  transferForm!: FormGroup
  toast !: toastPayload
  Branches !: SelectList[]
  Structures !: SelectList[]
  Employees !: SelectList[]
  Documents: any

  caseState ! : ICaseState

  constructor(
    private activeModal: NgbActiveModal,
    private route: Router,
    private userService: UserService,
    private caseService: CaseService,
    private formBuilder: FormBuilder,
    private commonService: CommonService,
    private organizationService: OrganizationService) {

    this.transferForm = this.formBuilder.group({
      ToEmployeeId: [''],
      ToStructureId: ['', Validators.required],
      Remark: ['']
    })

  }
  ngOnInit(): void {

    this.user = this.userService.getCurrentUser()
    this.getBranches()
    this.getCaseState()
  }

  getCaseState (){

    this.caseService.GetCaseState(this.CaseTypeId,this.historyId).subscribe({
      next:(res)=>{
      this.caseState = res 

      }
      ,error:(err)=>{
        console.error(err)
      }
    })
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

    if (this.transferForm.valid) {


      const formData = new FormData();

      for (let file of this.Documents) {
        formData.append('attachments', file);
      }

      formData.set('CaseHistoryId', this.historyId)
      //formData.set('CaseTypeId',this)
      formData.set('FromEmployeeId', this.user.EmployeeId)
      formData.set('userId', this.user.UserID)
      formData.set('Remark', this.transferForm.value.Remark)
      formData.set('ToEmployeeId', this.transferForm.value.ToEmployeeId)
      formData.set('ToStructureId', this.transferForm.value.ToStructureId)

      this.caseService.TransferCase(formData).subscribe({
        next: (res) => {
          this.toast = {
            message: 'Case Transfred Successfully!!',
            title: 'Successfull.',
            type: 'success',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);
          this.route.navigate(['mycaselist'])
          this.closeModal()

        }, error: (err) => {

          this.toast = {
            message: 'Something went wrong!!',
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

  closeModal() {

    this.activeModal.close()
  }

  onFileSelected(event: any) {
    this.Documents = (event.target).files;

  }



}

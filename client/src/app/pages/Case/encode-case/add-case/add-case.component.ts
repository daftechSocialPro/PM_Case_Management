import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { SelectList } from 'src/app/pages/common/common';
import { CaseService } from '../../case.service';
import { AddApplicantComponent } from '../add-applicant/add-applicant.component';

@Component({
  selector: 'app-add-case',
  templateUrl: './add-case.component.html',
  styleUrls: ['./add-case.component.css']
})
export class AddCaseComponent implements OnInit {

  caseForm !: FormGroup;
  applicants !: SelectList[];
  outsideCases!: SelectList[]
  toast!: toastPayload
  CaseNumber!:string


  constructor(
    private activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private commonService: CommonService,
    private modalService: NgbModal,
    private caseService: CaseService
    // private caseService :     
  ) {

    this.caseForm = this.formBuilder.group({
     
      LetterNumber: ['', Validators.required],
      LetterSubject: ['', Validators.required],
      CaseTypeId: ['', Validators.required],
      ApplicantId: ['', Validators.required],
      PhoneNumber2: ['', Validators.required],
      Representative: ['', Validators.required]
    })


  }
  ngOnInit(): void {


    this.getCaseNumber()
    this.getApplicants()
    this.getOutSideCases()
  }

getCaseNumber(){

  this.caseService.getCaseNumber().subscribe({
    next: (res) => {
      console.log(res)
      this.CaseNumber = res
    }, error: (err) => {
      console.error(err)
    }
  })
}


  getOutSideCases() {
    this.caseService.getCaseTypeByCaseForm("Outside").subscribe({
      next: (res) => {
        this.outsideCases = JSON.parse(res)
      }, error: (err) => {
        console.error(err)
      }
    })
  }
  getApplicants() {

    this.caseService.getApplicantSelectList().subscribe({
      next: (res) => {
        this.applicants = res
      },
      error: (err) => {
        console.error(err)
      }
    })
  }
  submit() {

    if (this.caseForm.valid) {


      this.caseService.addCase(this.caseForm.value).subscribe({
        next: (res) => {
          this.toast = {
            message: ' Case Successfully Created',
            title: 'Successfully Created.',
            type: 'success',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);
          this.closeModal()

        }, error: (err) => {
          this.toast = {
            message: err.message,
            title: 'Something went wrong.',
            type: 'error',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);
          console.log(err)
        }
      })

    }
    else {

    }

  }


  addApplicant() {
    let modalRef = this.modalService.open(AddApplicantComponent, { size: 'lg', backdrop: 'static' })
  }

  closeModal() {

    this.activeModal.close()
  }

}

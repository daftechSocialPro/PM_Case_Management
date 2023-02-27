import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { SelectList } from 'src/app/pages/common/common';
import { UserView } from 'src/app/pages/pages-login/user';
import { UserService } from 'src/app/pages/pages-login/user.service';
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
  outsideCases!: SelectList[];
  fileSettings! :SelectList[]; 
  toast!: toastPayload
  CaseNumber!:string
  Documents:any;
  settingsFile : fileSettingSender [] =[];

  user!: UserView


  constructor(
    private activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private commonService: CommonService,
    private modalService: NgbModal,
    private caseService: CaseService,
    private userService : UserService
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

this.user = this.userService.getCurrentUser()
    this.getCaseNumber()
    this.getApplicants()
    this.getOutSideCases()
  }

getCaseNumber(){

  this.caseService.getCaseNumber().subscribe({
    next:(res)=>{
      this.CaseNumber= res
    },error:(err)=>{
      console.error(err)
    }
  })

}


  getOutSideCases() {
    this.caseService.getCaseTypeByCaseForm("Outside").subscribe({
      next: (res) => {
        
        this.outsideCases = res 
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

  getFileSettings(casetTypeId : string){

  this.caseService.getFileSettignsByCaseTypeId(casetTypeId).subscribe({
    next:(res)=>{
      this.fileSettings = res 
    },error:(err)=>{
      console.error(err)
    }
  })
  }
  submit() {

    if (this.caseForm.valid) {

      const formData = new FormData();

      for(let file of this.Documents){
        formData.append('attachments',file);        
      }
      for (let file of this.settingsFile){

        formData.append('fileSettings',file.File,file.FileSettingId)
      }

      formData.set('CaseNumber',this.CaseNumber)
      formData.set('LetterNumber',this.caseForm.value.LetterNumber)
      formData.set('LetterSubject',this.caseForm.value.LetterSubject)
      formData.set('CaseTypeId',this.caseForm.value.CaseTypeId)
      formData.set('ApplicantId',this.caseForm.value.ApplicantId)
      formData.set('PhoneNumber2',this.caseForm.value.PhoneNumber2)
      formData.set('Representative',this.caseForm.value.Representative)
      formData.set('CreatedBy',this.user.UserID)

      //console.log(formData)
     
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

    modalRef.result.then(()=>{
      this.getApplicants()
    })

  }



  onFileSelected(event:any){
    this.Documents = (event.target).files;
    
   }
   onFileSettongSelected( filesettingId :string,event:any){

    var settingFile :fileSettingSender={
      FileSettingId : filesettingId,
      File:(event.target).files[0]
    }

    if (this.settingsFile.filter(x=>x.FileSettingId === filesettingId).length>0){

      const indexfile = this.settingsFile.findIndex(f=>f.FileSettingId===filesettingId)

      this.settingsFile.splice(indexfile,1)
      this.settingsFile.push(settingFile)

    }
    else {
      this.settingsFile.push(settingFile)
    }

   

   }





  closeModal() {

    this.activeModal.close()
  }

}

export interface fileSettingSender {

  FileSettingId : string ;
  File : File

  
}
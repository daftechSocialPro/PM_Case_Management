import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { CaseService } from '../../case.service';

@Component({
  selector: 'app-add-case',
  templateUrl: './add-case.component.html',
  styleUrls: ['./add-case.component.css']
})
export class AddCaseComponent implements OnInit {

  caseForm ! : FormGroup;
  toast !: toastPayload

  constructor(
    private activeModal : NgbActiveModal,
    private formBuilder :FormBuilder,
    private commonService : CommonService,
    private caseService : CaseService    
    )
    {

this.caseForm = this.formBuilder.group({
  CaseNumber : ['',Validators.required],
  LetterNumber : ['', Validators.required]
})


    }
  ngOnInit(): void {
  
    
  }
  submit(){

    if( this.caseForm.valid){


      this.caseService.addCase(this.caseForm.value).subscribe({
        next:(res)=>{
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

        },error:(err)=>{
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
      alert()
    }

  }

  closeModal(){

    this.activeModal.close()
  }

}

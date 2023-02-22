import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';

@Component({
  selector: 'app-add-case-type',
  templateUrl: './add-case-type.component.html',
  styleUrls: ['./add-case-type.component.css']
})
export class AddCaseTypeComponent {
  caseForm ! : FormGroup;

  toast !: toastPayload;

  constructor(
    private activeModal : NgbActiveModal,
    private formBuilder :FormBuilder,
    private commonService: CommonService){

this.caseForm = this.formBuilder.group({
  CaseTypeTittle : ['',Validators.required],
  TotalPayment : [0, Validators.required],
  TotaLength : [0, Validators.required],
  MesurementUnit : ['', Validators.required],
  SMSCode:['', Validators.required],
  casetype:['', Validators.required],
  Remark: ['', Validators.required],

})


    }
  ngOnInit(): void {
  
    
  }
  submit(){

    if( this.caseForm.valid){
      
          this.toast = {
            message: "case type Successfully Creted",
            title: 'Successfully Created.',
            type: 'success',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);
          this.closeModal()

      

          this.toast = {
            message: 'err',
            title: 'Network error.',
            type: 'error',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);

     
      

      console.log(this.caseForm.value)

    }
    else {
      alert()
    }

  }

  closeModal(){

    this.activeModal.close()
  }


}

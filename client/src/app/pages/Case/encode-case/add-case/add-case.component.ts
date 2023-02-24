import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from 'src/app/common/common.service';

@Component({
  selector: 'app-add-case',
  templateUrl: './add-case.component.html',
  styleUrls: ['./add-case.component.css']
})
export class AddCaseComponent implements OnInit {

  caseForm ! : FormGroup;

  constructor(
    private activeModal : NgbActiveModal,
    private formBuilder :FormBuilder,
    private commonService : CommonService,
   // private caseService :     
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

    }
    else {
      alert()
    }

  }

  closeModal(){

    this.activeModal.close()
  }

}

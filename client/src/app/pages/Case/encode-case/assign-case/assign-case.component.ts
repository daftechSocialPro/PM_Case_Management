import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-assign-case',
  templateUrl: './assign-case.component.html',
  styleUrls: ['./assign-case.component.css']
})

export class AssignCaseComponent implements OnInit {
  caseForm ! : FormGroup;

  constructor(
    private activeModal : NgbActiveModal,
    private formBuilder :FormBuilder){

this.caseForm = this.formBuilder.group({
  selectwho: ['',Validators.required],
  Structure : ['', Validators.required],
  ForEmployee : ['', Validators.required],
  CCto: ['', Validators.required]
})



    }
  ngOnInit(): void {
  
    
  }

  submit(){

    if( this.caseForm.valid){
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

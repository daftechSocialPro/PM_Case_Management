import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-accept-reject',
  templateUrl: './accept-reject.component.html',
  styleUrls: ['./accept-reject.component.css']
})
export class AcceptRejectComponent implements OnInit {
@Input() progressId!: string
@Input() userType!: string
@Input() actiontype!: string
acceptForm !: FormGroup

ngOnInit(): void {
  
}
constructor ( private formBuilder : FormBuilder, private activeModal : NgbActiveModal){

  this.acceptForm = this.formBuilder.group({

  Remark : ['']

  })
}

closeModal(){
  this.activeModal.close()
}

submit (){

  if (this.acceptForm.valid){
    console.log('value',this.acceptForm.value)
  }
}


}

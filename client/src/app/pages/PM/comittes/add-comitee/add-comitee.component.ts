import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-comitee',
  templateUrl: './add-comitee.component.html',
  styleUrls: ['./add-comitee.component.css']
})
export class AddComiteeComponent implements OnInit {

  comiteeeForm!: FormGroup;
  

  constructor(
    private activeModal: NgbActiveModal,
    private formBuilder: FormBuilder
  ) {

    this.comiteeeForm = this.formBuilder.group({
      CommitteeName: ['', Validators.required],
      Remark : ['']
    })

  }


  ngOnInit(): void { }

  closeModal() {
    this.activeModal.close()
  }

  submit() {

  }

}

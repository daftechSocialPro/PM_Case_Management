import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-update-cpmmittee',
  templateUrl: './update-cpmmittee.component.html',
  styleUrls: ['./update-cpmmittee.component.css']
})
export class UpdateCpmmitteeComponent implements OnInit {

  comiteeeForm!: FormGroup;
  

  constructor(
    private activeModal: NgbActiveModal,
    private formBuilder: FormBuilder
  ) {

    this.comiteeeForm = this.formBuilder.group({
      CommitteeName: ['', Validators.required]
    })

  }


  ngOnInit(): void { }

  closeModal() {
    this.activeModal.close()
  }

  submit() {

  }
}

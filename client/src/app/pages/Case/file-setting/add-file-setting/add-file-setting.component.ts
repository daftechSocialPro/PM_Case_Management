import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add-file-setting',
  templateUrl: './add-file-setting.component.html',
  styleUrls: ['./add-file-setting.component.css']
})
export class AddFileSettingComponent {

  settingForm ! : FormGroup
  constructor (private activeModal: NgbActiveModal, 
    private formBuilder: FormBuilder){

    this.settingForm = this.formBuilder.group({
      AffairTypeId : ['',Validators.required],
      FileName : [0, Validators.required],
      FileType : [0, Validators.required],
  

    })
    
    

  }
  ngOnInit(): void {
  
    
  }
  submit(){}
  closeModal(){

    this.activeModal.close()
  }


}

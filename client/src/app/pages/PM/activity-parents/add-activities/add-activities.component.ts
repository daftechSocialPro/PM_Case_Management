import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TaskView } from '../../tasks/task';

@Component({
  selector: 'app-add-activities',
  templateUrl: './add-activities.component.html',
  styleUrls: ['./add-activities.component.css']
})
export class AddActivitiesComponent implements OnInit{

  @Input() task!: TaskView;
  activityForm!:FormGroup; 

  constructor (
    private activeModal: NgbActiveModal,
    private formBuilder : FormBuilder
  ) {

    this.activityForm = this.formBuilder.group({
      
      ActivityDescription : ['',Validators.required],
      StartDate : ['',Validators.required],
      EndDate : ['',Validators.required],
      PlannedBudget : [0 , Validators.required],
      Weight : [0, Validators.required],
      ActivityType: ['',Validators.required],
      OfficeWork :[0,Validators.required],
      FieldWOrk :[0,Validators.required],
      UnitofMeasurment : ['',Validators.required],
      Performance : [0,Validators.required],
      Target :[0,Validators.required],
      


    })
  }
  ngOnInit(): void {
   
  }

  submit(){
    
  }

  closeModal(){

    this.activeModal.close()
  }

  
}

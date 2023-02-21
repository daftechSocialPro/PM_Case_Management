import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { SelectList } from 'src/app/pages/common/common';
import { TaskView } from '../../tasks/task';
declare const $: any 

@Component({
  selector: 'app-add-activities',
  templateUrl: './add-activities.component.html',
  styleUrls: ['./add-activities.component.css']
})
export class AddActivitiesComponent implements OnInit{

  @Input() task!: TaskView;
  activityForm!:FormGroup; 
  selectedEmployee: SelectList[] = [];
  
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
      WhomToAssign:[0,Validators.required]
      


    })
  }
  ngOnInit(): void {

    var calendar = $.calendars.instance('ethiopian','am');
    $('#StartDate').calendarsPicker({calendar: calendar});

    // $('.stdate').calendarsPicker({
    //   calendar: $.calendars.instance('ethiopian', 'am'),
      
    //   // onSelect: function (dates) {
    //   //   this.dateee = dates;
    //   //   if (this.dateee[0]) {
    //   //     self.driver.appointmentGivenDate = ${this.dateee[0]._day}-${this.dateee[0]._month}-${this.dateee[0]._year};
    //   //   }
    //   // },
    // })
   
  }

  selectEmployee(event: SelectList) {

    this.selectedEmployee.push(event)
    this.task.TaskMembers = this.task.TaskMembers!.filter(x => x.Id != event.Id)

  }

  removeSelected(emp: SelectList) {
    
    this.selectedEmployee = this.selectedEmployee.filter(x => x.Id != emp.Id)
    this.task.TaskMembers!.push(emp)

  }

  submit(){
    
  }

  closeModal(){

    this.activeModal.close()
  }

  
}

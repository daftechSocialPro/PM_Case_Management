import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { SelectList } from 'src/app/pages/common/common';
import { OrganizationService } from 'src/app/pages/common/organization/organization.service';
import { UserView } from 'src/app/pages/pages-login/user';
import { UserService } from 'src/app/pages/pages-login/user.service';
import { PMService } from '../../pm.services';
import { TaskView } from '../../tasks/task';
import {  ActivityDetailDto, SubActivityDetailDto } from './add-activities';
declare const $: any

@Component({
  selector: 'app-add-activities',
  templateUrl: './add-activities.component.html',
  styleUrls: ['./add-activities.component.css']
})
export class AddActivitiesComponent implements OnInit {

  @Input() task!: TaskView;
  activityForm!: FormGroup;
  selectedEmployee: SelectList[] = [];
  user !: UserView;
  committees: SelectList[] = [];
  unitMeasurments: SelectList[] = [];
  StartDate: string = "";
  EndDate: string = "";

  toast!: toastPayload



  constructor(
    private activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private pmService: PMService,
    private orgService: OrganizationService,
    private commonService: CommonService
  ) {

    this.activityForm = this.formBuilder.group({

      ActivityDescription: ['', Validators.required],
      PlannedBudget: [0, Validators.required],
      Weight: [0, Validators.required],
      ActivityType: [''],
      OfficeWork: [0, Validators.required],
      FieldWork: [0, Validators.required],
      UnitOfMeasurement: ['', Validators.required],
      PreviousPerformance: [0, Validators.required],
      Goal: [0, Validators.required],
      WhomToAssign: [''],
      TeamId: [null],
      CommiteeId: [null],
      AssignedEmployee: []


    })
  }
  ngOnInit(): void {

    this.user = this.userService.getCurrentUser()

    this.pmService.getComitteeSelectList().subscribe({
      next: (res) => {
        this.committees = res
      }, error: (err) => {
        console.log(err)
      }
    })

    this.orgService.getUnitOfMeasurmentSelectList().subscribe({
      next: (res) => {
        this.unitMeasurments = res
      }, error: (err) => {
        console.log(err)
      }
    })


    $('#StartDate').calendarsPicker({
      calendar: $.calendars.instance('ethiopian', 'am'),

      onSelect: (date: string) => {
        this.StartDate = date

        alert(this.StartDate)
      },
    })
    $('#EndDate').calendarsPicker({
      calendar: $.calendars.instance('ethiopian', 'am'),

      onSelect: (date: string) => {

        this.EndDate = date
      },
    })

  }

  selectEmployee(event: SelectList) {
    this.selectedEmployee.push(event)
    this.task.TaskMembers = this.task.TaskMembers!.filter(x => x.Id != event.Id)

  }

  removeSelected(emp: SelectList) {

    this.selectedEmployee = this.selectedEmployee.filter(x => x.Id != emp.Id)
    this.task.TaskMembers!.push(emp)

  }

  submit() {

    if (this.activityForm.valid) {

      let actvityP: SubActivityDetailDto []= [{

        SubActivityDesctiption: this.activityForm.value.ActivityDescription,
        StartDate: '1/11/2014', //this.StartDate,
        EndDate: '30/10/2015',//this.EndDate,
        PlannedBudget: this.activityForm.value.PlannedBudget,
        Weight: this.activityForm.value.Weight,
        ActivityType: this.activityForm.value.ActivityType,
        OfficeWork: this.activityForm.value.ActivityType == 0 ? this.activityForm.value.OfficeWork : this.activityForm.value.ActivityType == 1 ? 100 : 0,
        FieldWork: this.activityForm.value.ActivityType == 0 ? this.activityForm.value.FieldWork : this.activityForm.value.ActivityType == 2 ? 100 : 0,
        UnitOfMeasurement: this.activityForm.value.UnitOfMeasurement,
        PreviousPerformance: this.activityForm.value.PreviousPerformance,
        Goal: this.activityForm.value.Goal,
        TeamId: this.activityForm.value.TeamId,
        CommiteeId: this.activityForm.value.CommiteeId,
        Employees: this.activityForm.value.AssignedEmployee

      }]

      let addActivityDto: ActivityDetailDto = {
        ActivityDescription: this.activityForm.value.ActivityDescription,
        HasActivity: false,
        TaskId: this.task.Id!,
        CreatedBy: this.user.UserID,
        ActivityDetails: actvityP
      }
      console.log("activity detail", addActivityDto)


      this.pmService.addActivityParent(addActivityDto).subscribe({
        next: (res) => {
          this.toast = {
            message: ' Activity Successfully Created',
            title: 'Successfully Created.',
            type: 'success',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);
          this.closeModal()
        }, error: (err) => {
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

          console.error(err)
        }
      })

    }


  }

  closeModal() {
    this.activeModal.close()
  }


}

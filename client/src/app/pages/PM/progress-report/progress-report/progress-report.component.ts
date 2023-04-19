import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SelectList } from 'src/app/pages/common/common';
import { OrganizationService } from 'src/app/pages/common/organization/organization.service';
import { PMService } from '../../pm.services';
import { IPlannedReport } from '../planned-report/planned-report';

@Component({
  selector: 'app-progress-report',
  templateUrl: './progress-report.component.html',
  styleUrls: ['./progress-report.component.css']
})
export class ProgressReportComponent implements OnInit {

  serachForm!: FormGroup
  plannedreport  !: IPlannedReport

  cnt: number = 0
  programs !: SelectList[]
  plans !: SelectList[]
  tasks!: SelectList[]
  activities!: SelectList[]
  actparents!: SelectList[]


  constructor(
    private formBuilder: FormBuilder,
    private pmService: PMService,
    private orgService: OrganizationService) {

  }

  ngOnInit(): void {

    this.serachForm = this.formBuilder.group({
      BudgetYear: ['', Validators.required],
      programId: ['', Validators.required],
      planId: ['', Validators.required],
      taskId: [''],
      activityId: [''],
      ReportBy: ['Quarter']
    })



    this.pmService.getProgramSelectList().subscribe({
      next: (res) => {
        this.programs = res
        console.log(res)
      }, error: (err) => {
        console.error(err)
      }
    })


  }

  Search() {

    this.pmService.getPlannedReport(this.serachForm.value.BudgetYear, this.serachForm.value.ReportBy, this.serachForm.value.selectStructureId).subscribe({
      next: (res) => {
        this.plannedreport = res
        this.cnt = this.plannedreport?.pMINT

      }, error: (err) => {
        console.error(err)
      }
    })

  }


  onProgramChange(value: string) {

    this.pmService.getByProgramIdSelectList(value).subscribe({
      next: (res) => {
        this.plans = res
      },
      error: (err) => {
        console.error(err)
      }
    })
  }

  onPlanChange(value: string) {
    this.pmService.getByTaskIdSelectList(value).subscribe({
      next: (res) => {
        this.tasks = res
      },
      error: (err) => {
        console.error(err)
      }
    })

    this.onChangeActParent(value,undefined,undefined)
  }

  onTaskChange(value: string) {
    this.pmService.getActivitieParentsSelectList(value).subscribe({
      next: (res) => {
        this.actparents = res
      },
      error: (err) => {
        console.error(err)
      }
    })
    this.onChangeActParent(undefined,value,undefined)
  }

  onChangeActParent(planId?: string, taskId ?: string , actParentId?:string) {


    this.pmService.GetActivitiesSelectList(planId,taskId,actParentId).subscribe({
      next: (res) => {
        this.activities = res
      },
      error: (err) => {
        console.error(err)
      }

    })
  }

}

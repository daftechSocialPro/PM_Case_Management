import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PMService } from '../../pm.services';
import { SelectList } from 'src/app/pages/common/common';
import { IPlanReportDetailDto } from './IplanReportDetai';

@Component({
  selector: 'app-plan-report-today',
  templateUrl: './plan-report-today.component.html',
  styleUrls: ['./plan-report-today.component.css']
})
export class PlanReportTodayComponent implements OnInit {

  serachForm!: FormGroup
  planReportDetail  !: IPlanReportDetailDto
  cnt: number = 0
  programs !: SelectList[]
  constructor(private formBuilder: FormBuilder, private pmService: PMService) {

  }

  ngOnInit(): void {

    this.serachForm = this.formBuilder.group({
      BudgetYear: ['',Validators.required],
      ProgramId: ['',Validators.required],
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

    this.pmService.getPlanDetailReport(this.serachForm.value.BudgetYear, this.serachForm.value.ReportBy,this.serachForm.value.ProgramId).subscribe({
      next: (res) => {
        console.log("plan report",res)

       this.planReportDetail = res 
       this.cnt= res.MonthCounts.length

      }, error: (err) => {
        console.error(err)
      }
    })

  }
}
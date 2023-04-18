import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PMService } from '../../pm.services';
import { IPlanReportByProgramDto } from './program-budget-report';

@Component({
  selector: 'app-program-budget-report',
  templateUrl: './program-budget-report.component.html',
  styleUrls: ['./program-budget-report.component.css']
})
export class ProgramBudgetReportComponent implements OnInit {

  serachForm!: FormGroup
  PlanReportByProgramDto!:IPlanReportByProgramDto
  cnt:number = 0
  constructor(private formBuilder : FormBuilder,private pmService: PMService){

  }

  ngOnInit(): void {

    this.serachForm = this.formBuilder.group({
      BudgetYear: [''],
      ReportBy: ['Quarter']
    })

  

    
  }

  Search(){

    this.pmService.getProgramBudegtReport(this.serachForm.value.BudgetYear,this.serachForm.value.ReportBy).subscribe({
      next:(res)=>{
        console.log(res)
     
        this.PlanReportByProgramDto=res
        this.cnt=this.PlanReportByProgramDto?.MonthCounts.length
     
      },error:(err)=>{
        console.error(err)
      }
    })

  }
}

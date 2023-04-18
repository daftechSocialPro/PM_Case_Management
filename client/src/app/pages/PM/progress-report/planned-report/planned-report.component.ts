import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SelectList } from 'src/app/pages/common/common';
import { PMService } from '../../pm.services';
import { IPlanReportDetailDto } from '../plan-report-today/IplanReportDetai';
import { OrganizationService } from 'src/app/pages/common/organization/organization.service';
import { IPlannedReport } from './planned-report';

@Component({
  selector: 'app-planned-report',
  templateUrl: './planned-report.component.html',
  styleUrls: ['./planned-report.component.css']
})
export class PlannedReportComponent implements OnInit {

  serachForm!: FormGroup
  plannedreport  !: IPlannedReport
  branchs!: SelectList[]
  structures !: SelectList[]
  cnt: number = 0
  programs !: SelectList[]


  constructor(
    private formBuilder: FormBuilder,
    private pmService: PMService,
    private orgService: OrganizationService) {

  }

  ngOnInit(): void {

    this.serachForm = this.formBuilder.group({
      BudgetYear: ['', Validators.required],
      selectStructureId: ['', Validators.required],
      ReportBy: ['Quarter']
    })

    this.orgService.getOrgBranchSelectList().subscribe({
      next: (res) => {

        this.branchs = res
      }, error: (err) => {
        console.error(err)
      }
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
             this.cnt=  this.plannedreport?.pMINT

      }, error: (err) => {
        console.error(err)
      }
    })

  }

  OnBranchChange(branchId: string) {

    this.orgService.getOrgStructureSelectList(branchId).subscribe({
      next: (res) => {
        this.structures = res

      }, error: (err) => {
        console.error(err)
      }
    })

  }

  range(length: number) {
    return Array.from({ length }, (_, i) => i);
  }



}

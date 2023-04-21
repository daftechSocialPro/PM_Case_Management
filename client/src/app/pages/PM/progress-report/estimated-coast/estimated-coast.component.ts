import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SelectList } from 'src/app/pages/common/common';
import { OrganizationService } from 'src/app/pages/common/organization/organization.service';
import { PMService } from '../../pm.services';
import { IPlannedReport } from '../planned-report/planned-report';

@Component({
  selector: 'app-estimated-coast',
  templateUrl: './estimated-coast.component.html',
  styleUrls: ['./estimated-coast.component.css']
})
export class EstimatedCoastComponent implements OnInit {

  serachForm!: FormGroup
  estimatedCosts  !: any
  branchs!: SelectList[]
  structures !: SelectList[]
 

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

   


  }

  Search() {

    this.pmService.GetEstimatedCost(this.serachForm.value.selectStructureId,this.serachForm.value.BudgetYear).subscribe({
      next: (res) => {
             this.estimatedCosts = res 
             console.log(this.estimatedCosts)
             //this.cnt=  this.plannedreport?.pMINT

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
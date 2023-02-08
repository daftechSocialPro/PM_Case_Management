import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { toastPayload, CommonService } from 'src/app/common/common.service';
import { BudgetYearService } from 'src/app/pages/common/budget-year/budget-year.service';
import { SelectList } from 'src/app/pages/common/common';
import { OrganizationService } from 'src/app/pages/common/organization/organization.service';
import { Program } from '../../programs/Program';
import { ProgramService } from '../../programs/programs.services';

@Component({
  selector: 'app-add-plans',
  templateUrl: './add-plans.component.html',
  styleUrls: ['./add-plans.component.css']
})
export class AddPlansComponent implements OnInit {

  toast !: toastPayload;
  planForm!: FormGroup;
  employee!: SelectList;
  Programs: SelectList[] = [];
  Structures: SelectList[] = [];
  Employees: SelectList[] = [];
  BudgetYears: SelectList[] = [];
  Branchs: SelectList[] = [];
  employeeList: SelectList[] = [];
program! : Program


  constructor(
    private activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private budgetYearService: BudgetYearService,
    private programService: ProgramService,
    private commonService: CommonService,
    private orgService: OrganizationService) { }

  ngOnInit(): void {

    // this.budgetYearService.getProgramBudgetYearSelectList().subscribe({
    //   next: (res) => {
    //     console.log("res", res)
    //     this.programBudgetYears = res
    //   }, error: (err) => {

    //   }
    // })

    this.listEmployees();
    this.listPorgrams();
    this.listBranchs();
    this.planForm = this.formBuilder.group({

      PlanName: ['', Validators.required],
      BudgetYearId: ['', Validators.required],
      StructureId: ['', Validators.required],
      ProjectManagerId: ['', Validators.required],

      FinanceId: ['', Validators.required],
      ProgramId: ['', Validators.required],
      PlanWeight: [0, Validators.required],
      HasTask: [false, Validators.required],
      PlandBudget: [0, Validators.required],
      ProjectType: ['', Validators.required],
      Remark: ['']

    })

  }

  listStructuresbyBranchId(branchId: string) {

    this.orgService.getOrgStructureSelectList(branchId).subscribe({
      next: (res) => {
        this.Structures = res
      }, error: (err) => {
        console.log(err)
      }
    })
  }

  listBranchs() {

    this.orgService.getOrgBranchSelectList().subscribe({
      next: (res) => {
        this.Branchs = res
      }, error: (err) => {
        console.error(err)
      }
    })

  }

  listEmployees() {

    this.orgService.getEmployeesSelectList().subscribe({
      next: (res) => {
        this.employeeList = res
      }, error: (err) => {
        console.log(err)
      }
    })
  }


  listPorgrams() {
    this.programService.getProgramsSelectList().subscribe({
      next: (res) => {
        this.Programs = res
      },
      error: (err) => {
        console.error(err)
      }
    })
  }


  OnPorgramChange(value: string) {

    this.budgetYearService.getBudgetYearByProgramId(value).subscribe({
      next: (res) => {
        this.BudgetYears = res
      }, error: (err) => {
        console.error(err)
      }
    })

    this.programService.getProgramById(value).subscribe({
      next:(res)=>{
        this.program = res
      },
      error:(err)=>{
        console.error(err)
      }
    })

  }

  selectEmployee(event: SelectList) {
    this.employee = event
  }


  submit() {

    console.log("form", this.planForm.value)

    // if (this.planForm.valid) {

    //   this.programService.createProgram(this.programForm.value).subscribe({
    //     next: (res) => {
    //       this.toast = {
    //         message: "Program Successfully Creted",
    //         title: 'Successfully Created.',
    //         type: 'success',
    //         ic: {
    //           timeOut: 2500,
    //           closeButton: true,
    //         } as IndividualConfig,
    //       };
    //       this.commonService.showToast(this.toast);
    //       this.closeModal()

    //     }, error: (err) => {

    //       this.toast = {
    //         message: err,
    //         title: 'Network error.',
    //         type: 'error',
    //         ic: {
    //           timeOut: 2500,
    //           closeButton: true,
    //         } as IndividualConfig,
    //       };
    //       this.commonService.showToast(this.toast);

    //       console.log(err)
    //     }
    //   })
    // }

  }

  onBranchChange(branchId: string) {

     this.orgService.getOrgStructureSelectList(branchId).subscribe({
      next: (res) => {
        this.Structures = res
      }, error: (err) => {

        console.error(err)
      }

    })
  }

  closeModal() {
    this.activeModal.close();
  }

} 

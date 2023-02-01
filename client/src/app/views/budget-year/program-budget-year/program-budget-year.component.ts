import { Component, AfterViewInit, ViewChild } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder } from '@angular/forms';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/shared/common.service';
import { ProgramBudgetYear, PeriodicElement } from '../budget-year';
import { BudgetYearService } from '../budget-year.service'

import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';


@Component({
  selector: 'app-program-budget-year',
  templateUrl: './program-budget-year.component.html',
  styleUrls: ['./program-budget-year.component.scss']
})

export class ProgramBudgetYearComponent implements AfterViewInit {



  uploadForm!: UntypedFormGroup;
  toast!: toastPayload;

  programBudgetYears: ProgramBudgetYear[] = []
  displayedColumns: string[] = ['Name', 'FromYear', 'ToYear', 'Remark','Action'];
  dataSource = new MatTableDataSource<ProgramBudgetYear>();

  
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngAfterViewInit(): void {
    this.getAll()
  }


  getAll() {


    this.budgetYearService.getProgramBudgetYear().subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error: (err) => {
        alert("Error While fetching the Records!!")
      }
    })

  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  constructor(public fb: UntypedFormBuilder, private budgetYearService: BudgetYearService, private toastr: CommonService) {
    this.uploadForm = this.fb.group({

      Name: [''],
      FromYear: [0],
      ToYear: [0],
      Remark: ['']


    })

  }






  submit() {
    console.log(this.uploadForm.value)
    this.budgetYearService.CreateProgramBudgetYear(this.uploadForm.value).subscribe((res) => {


      this.toast = {
        message: 'Program Budget Year Successfully Created',
        title: 'Successfully Updated.',
        type: 'success',
        ic: {
          timeOut: 2500,
          closeButton: true,
        } as IndividualConfig,
      };
      this.toastr.showToast(this.toast);
      this.toggleCreateBudgetYear();
      //this.Reload();
      this.uploadForm = this.fb.group({

        Name: [''],
        PhoneNumber: [''],
        Address: [''],
        Remark: ['']
      })
    }
    );
  }


  createVisible = false

  toggleCreateBudgetYear() {
    this.createVisible = !this.createVisible;
  }


  handleCreateBudgetYearChange(event: any) {
    this.createVisible = event;
  }

}

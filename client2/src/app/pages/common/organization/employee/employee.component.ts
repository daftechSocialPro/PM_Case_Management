import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { CommonService } from 'src/app/common/common.service';

import { OrganizationService } from '../organization.service';
import { AddEmployeesComponent } from './add-employees/add-employees.component';
import { Employee } from './employee';
import { UpdateEmployeeComponent } from './update-employee/update-employee.component';



@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})

export class EmployeeComponent implements OnInit {

  employees: Employee[] = []

  constructor(private orgService: OrganizationService, private commonServcie: CommonService, private modalService: NgbModal) { }

  ngOnInit(): void {


    this.orgService.getEmployees().subscribe({
      next: (res) => {

        this.employees = res

        console.log("employees", this.employees)

      }, error: (err) => {
        console.error(err)
      }
    })
  }



  getPath(photo: string) {

    return this.commonServcie.createImgPath(photo)
  }

  updateEmp(emp: Employee) {

    let modalref = this.modalService.open(UpdateEmployeeComponent, { size: "lg", backdrop: 'static' })
    modalref.componentInstance.emp = emp

  }

  addModal() {
    this.modalService.open(AddEmployeesComponent, { size: 'lg', backdrop: 'static' })

  }


}



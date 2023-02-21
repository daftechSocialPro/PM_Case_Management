import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddComiteeComponent } from './add-comitee/add-comitee.component';
import { CommitteeView } from './committee';
import { CommitteeEmployeeComponent } from './committee-employee/committee-employee.component';
import { UpdateCpmmitteeComponent } from './update-cpmmittee/update-cpmmittee.component';

@Component({
  selector: 'app-comittes',
  templateUrl: './comittes.component.html',
  styleUrls: ['./comittes.component.css']
})
export class ComittesComponent implements OnInit {
  
  committees : CommitteeView[]=[
    {
      Id:'1',
      CommitteeName:'comittee 1 ',
      NoOfEmployee:8,
      Employees:[]
    }
  ]
  constructor (
    private modalService : NgbModal
  ){}
  ngOnInit(): void {
   
  }

  addCommitte(){

    let modalRef= this.modalService.open(AddComiteeComponent,{size:'md',backdrop:'static'})
  }

  employees(value : CommitteeView){

    let modalRef = this.modalService.open(CommitteeEmployeeComponent,{size:'xl',backdrop:'static'})
    modalRef.componentInstance.committee = value 


  }
  update(value : CommitteeView){

    let modalRef = this.modalService.open(UpdateCpmmitteeComponent,{size:'md',backdrop:'static'})
  }



}

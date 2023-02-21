import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddCaseTypeComponent } from './add-case-type/add-case-type.component';
@Component({
  selector: 'app-case-type',
  templateUrl: './case-type.component.html',
  styleUrls: ['./case-type.component.css']
})
export class CaseTypeComponent implements OnInit{
  constructor(private modalService: NgbModal){}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  addCaseType(){
    let modalRef = this.modalService.open(AddCaseTypeComponent,{size:'lg',backdrop:'static'})



  }

}

import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddProgramsComponent } from './add-programs/add-programs.component';

@Component({
  selector: 'app-programs',
  templateUrl: './programs.component.html',
  styleUrls: ['./programs.component.css']
})
export class ProgramsComponent {

  constructor(private modalService : NgbModal){}


  addProgram (){
  let modalRef =  this.modalService.open(AddProgramsComponent,{size:'xl',backdrop:'static'})

  

  }



}

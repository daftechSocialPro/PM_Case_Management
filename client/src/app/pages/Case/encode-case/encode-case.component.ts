import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddCaseComponent } from './add-case/add-case.component';

@Component({
  selector: 'app-encode-case',
  templateUrl: './encode-case.component.html',
  styleUrls: ['./encode-case.component.css']
})
export class EncodeCaseComponent implements OnInit {


constructor(private modalService: NgbModal){}
  ngOnInit(): void {
   
  }

  addCase(){

    let modalRef = this.modalService.open(AddCaseComponent,{size:'xl',backdrop:'static'})



  }




}

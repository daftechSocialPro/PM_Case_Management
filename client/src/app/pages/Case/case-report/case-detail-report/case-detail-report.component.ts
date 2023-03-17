import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DetailReportComponent } from './detail-report/detail-report.component';

@Component({
  selector: 'app-case-detail-report',
  templateUrl: './case-detail-report.component.html',
  styleUrls: ['./case-detail-report.component.css']
})
export class CaseDetailReportComponent implements OnInit {
  constructor(private modalService: NgbModal){
    
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

detail(){
let modalRef= this.modalService.open(DetailReportComponent,{size:"xl", backdrop: "static"})
}
}

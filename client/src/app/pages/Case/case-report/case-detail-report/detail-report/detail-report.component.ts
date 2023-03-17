import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-detail-report',
  templateUrl: './detail-report.component.html',
  styleUrls: ['./detail-report.component.css']
})
export class DetailReportComponent implements OnInit {
  
  constructor(private activeModal:NgbActiveModal){
    
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  closeModal(){
    this.activeModal.close();
  }

}

import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CaseService } from '../../case.service';
import { DetailReportComponent } from './detail-report/detail-report.component';
import { ICaseDetailReport } from './Icasedetail';

@Component({
  selector: 'app-case-detail-report',
  templateUrl: './case-detail-report.component.html',
  styleUrls: ['./case-detail-report.component.css']
})
export class CaseDetailReportComponent implements OnInit {

  detailReports !: ICaseDetailReport[]
  constructor(private modalService: NgbModal, private caseService: CaseService) {

  }
  ngOnInit(): void {
    this.getDetailReports()
  }

  getDetailReports() {


    this.caseService.GetCaseDetailReport().subscribe({
      next: (res) => {

        this.detailReports = res
        console.log(res)

      }, error: (err) => {
        console.error(err)
      }
    })


  }

  detail(caseId : string) {
    let modalRef = this.modalService.open(DetailReportComponent, { size: "xl", backdrop: "static" })
    modalRef.componentInstance.CaseId = caseId
  }

  getChange(elapsTime: string) {
    var hours = Math.abs(Date.now() - new Date(elapsTime).getTime()) / 36e5;
   // alert(hours)
    
    return Math.round(hours);
  }
}

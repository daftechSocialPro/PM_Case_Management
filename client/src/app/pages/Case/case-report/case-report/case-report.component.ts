import { Component, OnInit } from '@angular/core';
import { CaseService } from '../../case.service';
import { ICaseReport } from '../ICaseReport';

@Component({
  selector: 'app-case-report',
  templateUrl: './case-report.component.html',
  styleUrls: ['./case-report.component.css']
})
export class CaseReportComponent implements OnInit {

  caseReports!: ICaseReport[]
  selectedCaseReport !:ICaseReport 
  constructor(private caseService: CaseService) {



  }

  ngOnInit(): void {

    this.caseService.getCaseReport().subscribe({
      next: (res) => {
        this.caseReports = res
      }, error: (err) => {
        console.error(err)
      }
    })
  }

  getChange(elapsTime:string){


    var hours = Math.abs(Date.now() - new Date(elapsTime).getTime() ) / 36e5;
   
    return Math.round(hours) ;
  }



}

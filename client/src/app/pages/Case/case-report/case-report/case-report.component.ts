import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { CaseService } from '../../case.service';
import { ICaseReport, ICaseReportChart } from '../ICaseReport';
declare const $: any
@Component({
  selector: 'app-case-report',
  templateUrl: './case-report.component.html',
  styleUrls: ['./case-report.component.css']
})
export class CaseReportComponent implements OnInit {


  data!: ICaseReportChart;
  data2 !: ICaseReportChart;
  serachForm!: FormGroup

  caseReports!: ICaseReport[]
  selectedCaseReport !: ICaseReport
  constructor(private caseService: CaseService, private formBuilder: FormBuilder) {
    this.serachForm = this.formBuilder.group({
      startDate: [''],
      endDate: ['']
    })
  }

  ngOnInit(): void {

    $('#startDate').calendarsPicker({
      calendar: $.calendars.instance('ethiopian', 'am'),
      onSelect: (date: any) => {
        debugger

        if (date) {

          this.serachForm.controls['startDate'].setValue(date[0]._month + "/" + date[0]._day + "/" + date[0]._year)


        }// this.StartDate = date


      },
    })
    $('#endDate').calendarsPicker({
      calendar: $.calendars.instance('ethiopian', 'am'),
      onSelect: (date: any) => {
        debugger

        if (date) {

          this.serachForm.controls['endDate'].setValue(date[0]._month + "/" + date[0]._day + "/" + date[0]._year)


        }// this.StartDate = date


      },
    })




    this.getCaseReport(this.serachForm.value.startDate, this.serachForm.value.endDate)
    this.getCaseReportChart(this.serachForm.value.startDate, this.serachForm.value.endDate)
    this.getCaseReportChartByStatus(this.serachForm.value.startDate, this.serachForm.value.endDate)




  }

  getChange(elapsTime: string) {
    var hours = Math.abs(Date.now() - new Date(elapsTime).getTime()) / 36e5;
    return Math.round(hours);
  }


  getCaseReport(startAt?: string, endAt?: string) {
    this.caseService.GetCaseReport(startAt, endAt).subscribe({
      next: (res) => {
        this.caseReports = res
      }, error: (err) => {
        console.error(err)
      }
    })

  }

  getCaseReportChart(startAt?: string, endAt?: string) {

    this.caseService.GetCaseReportChart(startAt, endAt).subscribe({
      next: (res) => {
        this.data = res;
      }, error: (err) => {
        console.error(err)
      }
    })

  }

  getCaseReportChartByStatus(startAt?: string, endAt?: string) {

    this.caseService.GetCaseReportChartByStatus(startAt, endAt).subscribe({
      next: (res) => {
        this.data2 = res
      }, error: (err) => {
        console.error(err)
      }
    })

  }


  Search() {

    this.getCaseReport(this.serachForm.value.startDate, this.serachForm.value.endDate)
    this.getCaseReportChart(this.serachForm.value.startDate, this.serachForm.value.endDate)
    this.getCaseReportChartByStatus(this.serachForm.value.startDate, this.serachForm.value.endDate)


  }



}

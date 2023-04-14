import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
declare const $: any
@Component({
  selector: 'app-pm-dashboard',
  templateUrl: './pm-dashboard.component.html',
  styleUrls: ['./pm-dashboard.component.css']
})
export class PmDashboardComponent implements OnInit {

  serachForm!: FormGroup
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
  }

  Search(){
    
  }

  }

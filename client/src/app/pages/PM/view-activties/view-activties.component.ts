import { formatDate } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { ActivityTargetComponent } from './activity-target/activity-target.component';

import { ActivityView } from './activityview';

@Component({
  selector: 'app-view-activties',
  templateUrl: './view-activties.component.html',
  styleUrls: ['./view-activties.component.css']
})
export class ViewActivtiesComponent implements OnInit {

  @Input() actView!: ActivityView;
  
  months: string[] = [
    'August (ነሃሴ)',
    'September (መስከረም)',
    'October (ጥቅምት)',
    'November (ህዳር)',
    'December (ታህሳስ)',
    'January (ጥር)',
    'February (የካቲት)',
    'March (መጋቢት)',
    'April (ሚያዚያ)',
    'May (ግንቦት)',
    'June (ሰኔ)'
  ];

  constructor(private modalService: NgbModal) { }

  ngOnInit(): void {
    console.log("task", this.actView)
  }


  AssignTarget() {
    let modalRef = this.modalService.open(ActivityTargetComponent, { size: 'xl', backdrop: 'static' })
    modalRef.componentInstance.activity = this.actView
  }





}

import { formatDate } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from 'src/app/common/common.service';
import { UserView } from '../../pages-login/user';
import { UserService } from '../../pages-login/user.service';

import { ActivityTargetComponent } from './activity-target/activity-target.component';

import { ActivityView } from './activityview';
import { AddProgressComponent } from './add-progress/add-progress.component';
import { ViewProgressComponent } from './view-progress/view-progress.component';

@Component({
  selector: 'app-view-activties',
  templateUrl: './view-activties.component.html',
  styleUrls: ['./view-activties.component.css']
})
export class ViewActivtiesComponent implements OnInit {

  @Input() actView!: ActivityView;
  isMember: boolean = false;
  user!: UserView;

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

  constructor(
    private modalService: NgbModal,
    private commonService: CommonService,
    private userService: UserService) { }

  ngOnInit(): void {
    console.log("task", this.actView)
    this.user = this.userService.getCurrentUser()
    if (this.actView.Members.find(x => x.EmployeeId?.toLowerCase() == this.user.EmployeeId.toLowerCase())) {
      this.isMember = true;
    }
  }
  getImage(value: string) {
    return this.commonService.createImgPath(value)
  }


  AssignTarget() {
    let modalRef = this.modalService.open(ActivityTargetComponent, { size: 'xl', backdrop: 'static' })
    modalRef.componentInstance.activity = this.actView
  }

  AddProgress(){
    let modalRef = this.modalService.open(AddProgressComponent,{size:'lg',backdrop:'static'})
    modalRef.componentInstance.activity =this.actView
  }

  ViewProgress(){

    let modalRef = this.modalService.open(ViewProgressComponent,{size:'xl',backdrop:'static'})
    modalRef.componentInstance.activity = this.actView

  }





}

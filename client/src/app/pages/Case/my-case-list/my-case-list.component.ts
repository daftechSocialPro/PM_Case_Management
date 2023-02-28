import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserView } from '../../pages-login/user';
import { UserService } from '../../pages-login/user.service';
import { CaseService } from '../case.service';
import { ICaseView } from '../encode-case/Icase';

@Component({
  selector: 'app-my-case-list',
  templateUrl: './my-case-list.component.html',
  styleUrls: ['./my-case-list.component.css']
})
export class MyCaseListComponent implements OnInit {

  myacaselist!: ICaseView[]
  user!: UserView

  constructor(private modalService: NgbModal, private caseService: CaseService, private userService: UserService) { }
  ngOnInit(): void {
    this.user = this.userService.getCurrentUser()
    this.getMyCaseList()
  }

  getMyCaseList() {
    this.caseService.getMyCaseList(this.user.EmployeeId).subscribe({
      next: (res) => {
        this.myacaselist = res
      }, error: (err) => {
        console.error(err)
      }
    })
  }

}
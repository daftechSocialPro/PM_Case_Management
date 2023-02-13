import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PlanService } from '../plans/plan.service';
import { PlanSingleview } from '../plans/plans';
import { AddTasksComponent } from './add-tasks/add-tasks.component';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {


  plan!: PlanSingleview
  planId: String = ""

  constructor(private planService: PlanService, private route: ActivatedRoute, private modalService: NgbModal) { }
  ngOnInit(): void {
  

    this.planId = this.route.snapshot.paramMap.get('planId')!
    this.ListTask();
  }

  ListTask() {

    this.planService.getSinglePlans(this.planId).subscribe({
      next: (res) => {
        console.log("task",res)
        this.plan = res
      }
    })
  }

  addTask() {

    let modalRef = this.modalService.open(AddTasksComponent, { size: 'xl', backdrop: 'static' })
    modalRef.componentInstance.plan = this.plan
    modalRef.result.then((res) => {
      this.ListTask();
    })

  }
}
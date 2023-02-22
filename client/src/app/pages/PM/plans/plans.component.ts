import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { ProgramService } from '../programs/programs.services';
import { AddPlansComponent } from './add-plans/add-plans.component';
import { PlanService } from './plan.service';
import { PlanView } from './plans';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit {

  Plans: PlanView[] = []
  constructor(
    private modalService: NgbModal,
    private planService: PlanService,
    private router: Router) { }
  ngOnInit(): void {

    this.listPlans()
  }

  listPlans() {

    this.planService.getPlans().subscribe({
      next: (res) => {
        console.log("projects", res)
        this.Plans = res
      },
      error: (err) => {
        console.error(err)
      }
    })

  }



  addPlan() {

    let modalRef = this.modalService.open(AddPlansComponent, { size: 'xl', backdrop: 'static' });
    modalRef.result.then((res) => {
      this.listPlans()
    })

  }

  tasks(plan: PlanView) {

    const planId = plan ? plan.Id : null
    this.router.navigate(['task',{planId:planId}])
  
  }

}

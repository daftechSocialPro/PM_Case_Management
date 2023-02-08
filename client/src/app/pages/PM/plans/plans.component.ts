import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { ProgramService } from '../programs/programs.services';
import { AddPlansComponent } from './add-plans/add-plans.component';
import { PlanService } from './plan.service';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit {

  // Programs: Program[] = []
  constructor(private modalService: NgbModal, private planService: PlanService) { }
  ngOnInit(): void {

    this.listPlans()
  }

  listPlans() {

    // this.programService.getPrograms().subscribe({
    //   next: (res) => {
    //     this.Programs = res
    //   },
    //   error: (err) => {
    //     console.error(err)
    //   }
    // })

  }



  addPlan() {
    
    let modalRef = this.modalService.open(AddPlansComponent, { size: 'xl', backdrop: 'static' });
    modalRef.result.then((res) => {
      this.listPlans()
    })

  }

}

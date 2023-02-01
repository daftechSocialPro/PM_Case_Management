import { Component, OnInit } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder } from '@angular/forms';

@Component({
  selector: 'app-budget-year',
  templateUrl: './budget-year.component.html',
  styleUrls: ['./budget-year.component.scss']
})
export class BudgetYearComponent implements OnInit {


 
  ngOnInit(): void {



  }
  constructor(public fb: UntypedFormBuilder) {
  }


}

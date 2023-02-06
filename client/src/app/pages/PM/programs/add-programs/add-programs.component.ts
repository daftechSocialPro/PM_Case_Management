import { Component,OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {FormGroup,FormBuilder,Validators} from '@angular/forms'
import { ProgramBudgetYear, SelectList } from 'src/app/pages/common/common';
import { BudgetYearService } from 'src/app/pages/common/budget-year/budget-year.service';
@Component({
  selector: 'app-add-programs',
  templateUrl: './add-programs.component.html',
  styleUrls: ['./add-programs.component.css']
})
export class AddProgramsComponent implements OnInit {

  programForm!:FormGroup;
  programBudgetYears! : SelectList[];

  constructor(private activeModal: NgbActiveModal,private formBuilder : FormBuilder,private budgetYearService : BudgetYearService) { }
  
  ngOnInit(): void {

    this.budgetYearService.getProgramBudgetYearSelectList().subscribe({
      next:(res)=>{
        console.log("res",res)
        this.programBudgetYears = res 
      },error:(err)=>{

      }
    })
  
    this.programForm = this.formBuilder.group({

      ProgramBudgetYearId :['',Validators.required],
      ProgramName : ['',Validators.required],
      PlannedBudget : [0,Validators.required],
      Remark : ['']

    })
    
  }

  submit(){
    
  }

  closeModal() {
    this.activeModal.close();
  }


}

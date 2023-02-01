import { Component, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { BudgetYearwithoutId, ProgramBudgetYear } from '../../common';
import { BudgetYearService } from '../budget-year.service';

@Component({
  selector: 'app-add-budgetyear',
  templateUrl: './add-budgetyear.component.html',
  styleUrls: ['./add-budgetyear.component.css']
})
export class AddBudgetyearComponent implements OnInit {

  toast!: toastPayload;
  @Input() programBudget!: ProgramBudgetYear;
  BudgetYearForm!: FormGroup;
  @Output() result: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private budgetYearService: BudgetYearService,
    private commonService: CommonService,
    private activeModal: NgbActiveModal) {

    this.BudgetYearForm = this.formBuilder.group({

      Year: [null, Validators.required],
      Fromdate: [null, Validators.required],
      ToDate: [null, Validators.required],
      Remark: ['']
      
    })

  }

  ngOnInit(): void {



  }


  submit() {

    if (this.BudgetYearForm.valid) {

      let by: BudgetYearwithoutId = {

        Year: this.BudgetYearForm.value.year,
        FromDate: this.BudgetYearForm.value.Fromdate,
        ToDate: this.BudgetYearForm.value.ToDate,
        Remark: this.BudgetYearForm.value.Remark,
        ProgramBudgetYearId: this.programBudget!.Id,

      }

      this.budgetYearService.CreateBudgetYear(by).subscribe({

        next: (res) => {

          this.toast = {
            message: ' Budget Year Successfully Created',
            title: 'Successfully Created.',
            type: 'success',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);

          this.result = true;
          this.modalClose();
          this.BudgetYearForm = this.formBuilder.group({

            Year: [null],
            Fromdate: [null],
            ToDate: [null],
            Remark: ['']
          })

        }, error: (err) => {
          this.toast = {
            message: err,
            title: 'Network error.',
            type: 'error',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);


        }
      }
      );


    }
    else {
      this.toast = {
        message: 'Please check your form',
        title: 'Form Error.',
        type: 'error',
        ic: {
          timeOut: 2500,
          closeButton: true,
        } as IndividualConfig,
      };
      this.commonService.showToast(this.toast);
    }

  }

  modalClose() {
    this.activeModal.close()
  }

}

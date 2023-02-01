import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder } from '@angular/forms';
import { IndividualConfig } from 'ngx-toastr';
import { toastPayload, CommonService } from 'src/app/shared/common.service';
import { OrganizationService } from '../../organization.service';

@Component({
  selector: 'app-branch-create',
  templateUrl: './branch-create.component.html',
  styleUrls: ['./branch-create.component.scss']
})

export class BranchCreateComponent {


  toast!: toastPayload;
  uploadForm!: UntypedFormGroup;

  @Input() createVisible!: boolean;
  @Output() createVisibleChange = new EventEmitter<boolean>();

  @Output() branchList: EventEmitter<any> = new EventEmitter();

  constructor(public fb: UntypedFormBuilder, private orgService: OrganizationService, private toastr: CommonService) {
    this.uploadForm = this.fb.group({

      Name: [''],
      PhoneNumber: [''],
      Address: [''],
      Remark: ['']
    })
  }

  submit() {


    this.orgService.OrgBranchCreate(this.uploadForm.value).subscribe((res) => {


      this.toast = {
        message: 'Organizational Branch Successfully Updated',
        title: 'Successfully Updated.',
        type: 'success',
        ic: {
          timeOut: 2500,
          closeButton: true,
        } as IndividualConfig,
      };
      this.toastr.showToast(this.toast);
      this.onModalClose();
      this.branchList.emit();
      this.uploadForm = this.fb.group({

        Name: [''],
        PhoneNumber: [''],
        Address: [''],
        Remark: ['']
      })
    }
    );

  }

  onModalClose(): void {
    this.createVisibleChange.emit(false)
  }



}

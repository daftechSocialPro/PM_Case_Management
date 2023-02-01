import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder } from '@angular/forms';
import { IndividualConfig } from 'ngx-toastr';
import { toastPayload, CommonService } from 'src/app/shared/common.service';
import { SelectList } from 'src/app/shared/model/select-list';
import { OrganizationService } from '../../organization.service';

@Component({
  selector: 'app-structure-create',
  templateUrl: './structure-create.component.html',
  styleUrls: ['./structure-create.component.scss']
})
export class StructureCreateComponent {

  toast!: toastPayload;
  uploadForm!: UntypedFormGroup;

  @Input() createVisible!: boolean;
  @Output() createVisibleChange = new EventEmitter<boolean>();

  branchList: SelectList[]=[]
  parentStructureList : SelectList[]=[]


  constructor(public fb: UntypedFormBuilder, private orgService: OrganizationService, private toastr: CommonService) {

    this.orgService.getOrgBranchSelectList().subscribe(res=>this.branchList= res)
    this.orgService.getOrgStructureSelectList().subscribe(res=> this.parentStructureList = res)

    this.uploadForm = this.fb.group({

      OrganizationBranchId: [''],
      ParentStructureId: [null],
      StructureName: [''],      
      Order:[0],
      Weight:[0],
      Remark: ['']

    })
  }

  submit() {

    console.log(this.uploadForm.value)

    this.orgService.OrgStructureCreate(this.uploadForm.value).subscribe((res) => {


      this.toast = {
        message: 'Organizational Structure Successfully Created',
        title: 'Successfully Created.',
        type: 'success',
        ic: {
          timeOut: 2500,
          closeButton: true,
        } as IndividualConfig,
      };
      this.toastr.showToast(this.toast);
      this.onModalClose();
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

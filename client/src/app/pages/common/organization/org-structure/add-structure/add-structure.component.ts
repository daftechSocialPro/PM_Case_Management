import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { SelectList } from '../../../common';
import { OrganizationService } from '../../organization.service';

@Component({
  selector: 'app-add-structure',
  templateUrl: './add-structure.component.html',
  styleUrls: ['./add-structure.component.css']
})
export class AddStructureComponent implements OnInit {

  toast !: toastPayload;
  structureForm !: FormGroup;

  branchList: SelectList[]=[]
  parentStructureList : SelectList[]=[]

  constructor(private formBuilder: FormBuilder, private orgService: OrganizationService, private commonService: CommonService) {

    this.structureForm = this.formBuilder.group({
      OrganizationBranchId: [null, Validators.required],
      ParentStructureId: [null],
      StructureName: ['', Validators.required],
      Order: [null, Validators.required],
      Weight: [null, Validators.required],
      Remark: ['']
    })
  }

  ngOnInit(): void {
    this.orgService.getOrgBranchSelectList().subscribe(
      {
        next:(res)=>this.branchList= res,
        error:(err)=>console.error(err)
      })
 

  }

  onBranchChange(){

    this.orgService.getOrgStructureSelectList(this.structureForm.value.OrganizationBranchId).subscribe(
      {
        next:(res)=>this.parentStructureList = res,
        error:(err)=>console.error(err)

      }
      
      )
    
  }

  submit() {

    if (this.structureForm.valid) {
      this.orgService.OrgStructureCreate(this.structureForm.value).subscribe({

        next: (res) => {

          this.toast = {
            message: 'Organizational Structure Successfully Created',
            title: 'Successfully Created.',
            type: 'success',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);

          document.getElementById("ModalClose")?.click();
          this.structureForm = this.formBuilder.group({

            Name: [''],
            PhoneNumber: [''],
            Address: [''],
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
          document.getElementById("ModalClose")?.click();

        }
      }
      );
    }

  }
}

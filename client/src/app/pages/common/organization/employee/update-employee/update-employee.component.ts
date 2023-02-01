import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
//import { IndividualConfig } from 'ngx-toastr';
import { toastPayload, CommonService } from 'src/app/common/common.service';
import { SelectList } from '../../../common';
import { OrganizationService } from '../../organization.service';
import { Employee } from '../employee';

@Component({
  selector: 'app-update-employee',
  templateUrl: './update-employee.component.html',
  styleUrls: ['./update-employee.component.css']
})
export class UpdateEmployeeComponent implements OnInit {

  toast!: toastPayload;
  branchList: SelectList[] = [];
  parentStructureList: SelectList[] = [];
  
  emp !: Employee;


  EmployeeForm !: FormGroup;
  imageURL: string = "";


  constructor(private orgService: OrganizationService, private formBuilder: FormBuilder, private commonService: CommonService,private actvieModal:NgbActiveModal) {

  



  }

  ngOnInit(): void {

   this.imageURL = this.commonService.createImgPath(this.emp!.Photo)
    this.EmployeeForm = this.formBuilder.group({
      avatar: [null],
      // Photo: [null, Validators.required],
      Title: [this.emp!.Title, Validators.required],
      FullName: [this.emp!.FullName, Validators.required],    
      Gender: [this.emp!.Gender, Validators.required],
      PhoneNumber: [this.emp!.PhoneNumber, Validators.required],
      Position: [this.emp!.Position, Validators.required],
      StructureId: [this.emp!.StructureId, Validators.required],
      Remark: [this.emp!.Remark]

    })
    this.onBranchChange(this.emp!.BranchId);
    
   
    this.orgService.getOrgBranchSelectList().subscribe(
      {
        next: (res) => this.branchList = res,
        error: (err) => console.error(err)
      })


  }

  showPreview(event: any) {
    const file = (event.target).files[0];
    console.log(file)
    this.EmployeeForm.patchValue({
      avatar: file
    });
    this.EmployeeForm.get('avatar')?.updateValueAndValidity()
    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    }
    reader.readAsDataURL(file)
  }

  onBranchChange(value: string) {

    this.orgService.getOrgStructureSelectList(value).subscribe(
      {
        next: (res) => this.parentStructureList = res,
        error: (err) => console.error(err)

      })
  }

  submit() {




    if (this.EmployeeForm.valid) {

      var value = this.EmployeeForm.value;
      var file = value.avatar



      // if (files.length === 0)
      //   return;
      // let fileToUpload = <File>files[0];
      const formData = new FormData();
      file ? formData.append('file', file, file.name) : "";

      formData.set('Title', value.Title);
      formData.set('FullName', value.FullName);
      formData.set('Gender', value.Gender);
      formData.set('PhoneNumber', value.PhoneNumber);
      formData.set('Position', value.Position);
      formData.set('StructureId', value.StructureId);
      formData.set('Remark', value.Remark);


      this.orgService.employeeCreate(formData).subscribe({
        next: (res) => {
          this.toast = {
            message: 'Employee Successfully Created',
            title: 'Successfully Created.',
            type: 'success',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);

        }, error: (err) => {

          this.toast = {
            message: 'Something went wrong',
            title: 'Network error.',
            type: 'error',
            ic: {
              timeOut: 2500,
              closeButton: true,
            } as IndividualConfig,
          };
          this.commonService.showToast(this.toast);

          console.error(err)
        }
      })
    }
  }

  close(){
    this.actvieModal.close()
  }

}

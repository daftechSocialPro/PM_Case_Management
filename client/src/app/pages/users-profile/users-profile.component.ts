import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { Employee } from '../common/organization/employee/employee';
import { OrganizationService } from '../common/organization/organization.service';
import { UserView } from '../pages-login/user';
import { UserService } from '../pages-login/user.service';

@Component({
  selector: 'app-users-profile',
  templateUrl: './users-profile.component.html',
  styleUrls: ['./users-profile.component.css']
})
export class UsersProfileComponent implements OnInit {

  user !: UserView
  Employee!: Employee
  EmployeeForm!: FormGroup
  imageURL!: string
  toast !: toastPayload

  constructor(
    private commonService: CommonService,
    private userService: UserService,
    private orgServcie: OrganizationService,
    private formBuilder: FormBuilder) { 
      
    }

  ngOnInit(): void {
    this.user = this.userService.getCurrentUser()
    this.getEmployee();

    this.imageURL = this.commonService.createImgPath(this.user.Photo)
    this.EmployeeForm = this.formBuilder.group({
      avatar: [null],
      Title: [this.Employee?.Title, Validators.required],
      FullName: [this.Employee?.FullName, Validators.required],
      Gender: [this.Employee?.Gender, Validators.required],
      PhoneNumber: [this.Employee?.PhoneNumber, Validators.required],
      Remark: [this.Employee?.Remark]
    })





  }
  getEmployee() {

    this.orgServcie.GetEmployeesById(this.user.EmployeeId).subscribe({
      next: (res) => {
        this.Employee = res
      }, error: (err) => {
        console.error(err)
      }
    })
  }
  getImage(value: string) {

    return this.commonService.createImgPath(value)
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
  submit() {




    if (this.EmployeeForm.valid) {

      var value = this.EmployeeForm.value;
      var file = value.avatar



      // if (files.length === 0)
      //   return;
      // let fileToUpload = <File>files[0];
      const formData = new FormData();
      file ? formData.append('file', file, file.name) : "";

      formData.set('Id', this.Employee.Id)
      formData.set('Photo', this.Employee.Photo)
      formData.set('Title', value.Title);
      formData.set('FullName', value.FullName);
      formData.set('Gender', value.Gender);
      formData.set('PhoneNumber', value.PhoneNumber);

      formData.set('Remark', value.Remark);


      this.orgServcie.employeeUpdate(formData).subscribe({
        next: (res) => {
          this.toast = {
            message: 'Employee Successfully Updated',
            title: 'Successfully Updated.',
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

}

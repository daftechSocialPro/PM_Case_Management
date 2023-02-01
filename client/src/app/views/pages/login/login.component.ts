import { CommonService, toastPayload } from '../../../shared/common.service';
import { UserService } from '../../../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { NgForm, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IndividualConfig } from 'ngx-toastr';
import { Token } from './Model/user.model';
import { ValidationFormsService } from './validation.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [ValidationFormsService],
})
export class LoginComponent implements OnInit {

  toast!: toastPayload;

  simpleForm!: UntypedFormGroup;
  submitted = false;
  formErrors: any;

  constructor(private service: UserService, private router: Router, private toastr: CommonService, private fb: UntypedFormBuilder, public vf: ValidationFormsService) {

    this.createForm();
  }

  createForm() {
    this.simpleForm = this.fb.group(
      {

        username: [
          '',
          [
            Validators.required,
            
          ],
        ],

        password: [
          '',
          [
            Validators.required,
          
          ],
        ],
        
      },

    );
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.simpleForm.controls;
  }

  onReset() {
    this.submitted = false;
    this.simpleForm.reset();
  }

  onValidate() {
    this.submitted = true;

    // stop here if form is invalid
    return this.simpleForm.status === 'VALID';
  }


  onSubmit() {
    console.warn(this.onValidate(), this.simpleForm.value);

    if (this.onValidate()) {
      // TODO: Submit form value
      // console.warn(this.simpleForm.value);
      // alert('SUCCESS!');
      this.service.login(this.simpleForm.value).subscribe(
        (res: any) => {
          sessionStorage.setItem('token', res.token);
          this.router.navigateByUrl('/dashboard');
        },
        err => {
          if (err.status == 400){
            this.toast = {
              message: 'Incorrect username or password',
              title: 'Authentication failed.',
              type: 'error',
              ic: {
                timeOut: 2500,
                closeButton: true,
              } as IndividualConfig,
            };
            this.toastr.showToast(this.toast);
  
          }
            //this.toastr.showToast('Incorrect username or password. 'Authentication failed.');
          else
            console.log(err);
        }
      )





    }
  }


  ngOnInit() {
    if (sessionStorage.getItem('token') != null && sessionStorage.getItem('token')!="")
      this.router.navigateByUrl('/dashboard');
  }


}




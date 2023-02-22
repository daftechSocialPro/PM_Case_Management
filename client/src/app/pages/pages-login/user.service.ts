import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment';
import { Token, User, UserView } from './user';
import { SelectList } from '../common/common';
import { UserManagment } from '../common/user-management/user-managment';
import { Employee } from '../common/organization/employee/employee';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  readonly BaseURI = environment.baseUrl;


  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    //passwordMismatch
    // confirmPswrdCtrl!.errors= {passwordMismatch:true}
    if (confirmPswrdCtrl!.errors == null || 'passwordMismatch' in confirmPswrdCtrl!.errors) {
      if (fb.get('Password')!.value != confirmPswrdCtrl!.value)
        confirmPswrdCtrl!.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl!.setErrors(null);
    }
  }

  register(body: User) {

    return this.http.post(this.BaseURI + '/ApplicationUser/Register', body);
  }

  login(formData: User) {
    return this.http.post<Token>(this.BaseURI + '/ApplicationUser/Login', formData);
  }

  getUserProfile() {
    return this.http.get(this.BaseURI + '/UserProfile');
  }

  roleMatch(allowedRoles: any): boolean {
    var isMatch = false;
    var payLoad = JSON.parse(window.atob(sessionStorage.getItem('token')!.split('.')[1]));

    var userRole = payLoad.role;
    allowedRoles.forEach((element: any) => {
      if (userRole == element) {
        isMatch = true;
        return false;
      }
      else {
        return true;
      }
    });
    return isMatch;
  }

  getRoles (){

    return this.http.get<SelectList[]>(this.BaseURI+'/ApplicationUser/getroles')
  }

  getCurrentUser(){
    var payLoad = JSON.parse(window.atob(sessionStorage.getItem('token')!.split('.')[1]));

    let user : UserView={
      UserID : payLoad.UserID,
      FullName: payLoad.FullName,
      role : payLoad.role,
      EmployeeId:payLoad.EmployeeId
    }
    return user ; 
  }

  createUser (body:UserManagment){

    return this.http.post(this.BaseURI+"/ApplicationUser/Register",body)
  }

  getSystemUsers(){
    return this.http.get<Employee[]>(this.BaseURI+"/ApplicationUser/users")
  }
}

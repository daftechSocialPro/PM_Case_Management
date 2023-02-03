import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddUsersComponent } from './add-users/add-users.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent {

  constructor(private modalService: NgbModal){}

  addModal(){

    let modalRef = this.modalService.open(AddUsersComponent,{size:'lg',backdrop:'static'})
    modalRef.result.then((res)=>{
      
    })

  }

}

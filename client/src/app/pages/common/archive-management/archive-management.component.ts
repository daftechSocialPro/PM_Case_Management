import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddShelfComponent } from './add-shelf/add-shelf.component';

@Component({
  selector: 'app-archive-management',
  templateUrl: './archive-management.component.html',
  styleUrls: ['./archive-management.component.css']
})
export class ArchiveManagementComponent {


  constructor(private modalService: NgbModal) { }


  addShelf() {
    this.modalService.open(AddShelfComponent,{size:'lg',backdrop:'static'
    })
  }
}

import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddFileSettingComponent } from './add-file-setting/add-file-setting.component';

@Component({
  selector: 'app-file-setting',
  templateUrl: './file-setting.component.html',
  styleUrls: ['./file-setting.component.css']
})
export class FileSettingComponent implements OnInit {
  constructor( private modalService: NgbModal){}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  addfilesetting(){
    let modalRef = this.modalService.open(AddFileSettingComponent,{size:'lg',backdrop:'static'})
  }

}

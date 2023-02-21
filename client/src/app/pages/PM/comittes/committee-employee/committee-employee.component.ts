import { Component,Input,OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CommitteeView } from '../committee';

@Component({
  selector: 'app-committee-employee',
  templateUrl: './committee-employee.component.html',
  styleUrls: ['./committee-employee.component.css']
})
export class CommitteeEmployeeComponent implements OnInit {

@Input() committee!: CommitteeView ;
constructor ( private activeModal : NgbActiveModal){

}

  ngOnInit(): void {
    

  }

  closeModal(){
    this.activeModal.close()
  }

  

}

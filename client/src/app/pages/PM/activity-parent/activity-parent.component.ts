import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SelectList } from '../../common/common';
import { TaskView } from '../tasks/task';

@Component({
  selector: 'app-activity-parent',
  templateUrl: './activity-parent.component.html',
  styleUrls: ['./activity-parent.component.css']
})
export class ActivityParentComponent implements OnInit {

  task!: TaskView
  Employees !: SelectList[] 
  constructor(
    private activatedRoute :ActivatedRoute
  ){

  }

ngOnInit(): void {

  
  
}






}

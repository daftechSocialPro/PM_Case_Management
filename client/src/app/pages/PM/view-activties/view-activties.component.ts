import { Component, Input, OnInit } from '@angular/core';

import { ActivityView } from './activityview';

@Component({
  selector: 'app-view-activties',
  templateUrl: './view-activties.component.html',
  styleUrls: ['./view-activties.component.css']
})
export class ViewActivtiesComponent implements OnInit {

  @Input() actView!: ActivityView ; 
  ngOnInit(): void {
    
    console.log("task",this.actView)
    
  }

}

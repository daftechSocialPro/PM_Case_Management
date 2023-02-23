import { Component,Input,OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ActivityView } from '../activityview';

@Component({
  selector: 'app-add-progress',
  templateUrl: './add-progress.component.html',
  styleUrls: ['./add-progress.component.css']
})
export class AddProgressComponent implements OnInit {

  @Input() activity !: ActivityView ;
  months: string[] = [
    'July (ሃምሌ)',
    'August (ነሃሴ)',
    'September (መስከረም)',
    'October (ጥቅምት)',
    'November (ህዳር)',
    'December (ታህሳስ)',
    'January (ጥር)',
    'February (የካቲት)',
    'March (መጋቢት)',
    'April (ሚያዚያ)',
    'May (ግንቦት)',
    'June (ሰኔ)'
  ];

  constructor (private activeModal:NgbActiveModal){}
  ngOnInit(): void {
    
  }


  closeModal(){
    this.activeModal.close()
  }



}

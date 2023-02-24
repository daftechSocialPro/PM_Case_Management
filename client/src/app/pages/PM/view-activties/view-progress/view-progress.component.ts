import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CommonService } from 'src/app/common/common.service';
import { PMService } from '../../pm.services';
import { ActivityView, ViewProgressDto } from '../activityview';

@Component({
  selector: 'app-view-progress',
  templateUrl: './view-progress.component.html',
  styleUrls: ['./view-progress.component.css']
})
export class ViewProgressComponent implements OnInit {

  @Input() activity !: ActivityView;
  progress!:ViewProgressDto[]
  constructor(private activeModal: NgbActiveModal,private pmService : PMService,private commonService : CommonService) { }
  ngOnInit(): void { this.getProgress() }


  getProgress (){

    this.pmService.viewProgress(this.activity.Id).subscribe({
      next:(res)=>{
        this.progress = res
        console.log(res) 
      },
      error:(err)=>{
        console.log(err)
      }
    })

  }

  closeModal() {
    this.activeModal.close()
  }

  getFilePath (value:string){

    return this.commonService.createImgPath(value)

  }



}

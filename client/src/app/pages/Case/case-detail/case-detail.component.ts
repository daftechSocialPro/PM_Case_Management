import { Component,OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserView } from '../../pages-login/user';
import { UserService } from '../../pages-login/user.service';
import { CaseService } from '../case.service';
import { ICaseView } from '../encode-case/Icase';

@Component({
  selector: 'app-case-detail',
  templateUrl: './case-detail.component.html',
  styleUrls: ['./case-detail.component.css']
})
export class CaseDetailComponent implements OnInit {

  caseHistoryId!: string
  user!: UserView
  caseDetail!: ICaseView
  
  constructor(private caseService: CaseService, private userService: UserService,private router : ActivatedRoute){}
  ngOnInit(): void {
    
    this.user = this.userService.getCurrentUser()
    this.caseHistoryId = this.router.snapshot.paramMap.get('caseHistoryId')!
    this.getCaseDetail()
  }

  getCaseDetail (){
    this.caseService.GetCaseDetail(
      this.user.EmployeeId,
      this.caseHistoryId
     ).subscribe({
       next: (res) => {
        this.caseDetail = res 
       
    
       }, error: (err) => {
         console.error(err)
       }
     })
 
    
  }


}

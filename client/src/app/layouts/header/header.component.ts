import { Component, OnInit, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common'
import { AuthGuard } from 'src/app/auth/auth.guard';
import { PMService } from 'src/app/pages/pm/pm.services';
import { UserService } from 'src/app/pages/pages-login/user.service';
import { UserView } from 'src/app/pages/pages-login/user';
import { ActivityView } from 'src/app/pages/PM/view-activties/activityview';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  activites!: ActivityView[]
  user!: UserView

  constructor(@Inject(DOCUMENT) private document: Document,
   private authGuard: AuthGuard, 
   private pmService: PMService, 
   private userService: UserService,
   private router : Router) { }

  ngOnInit(): void {
    this.user = this.userService.getCurrentUser()

    this.getActivityForApproval()

    
  }


  
  getActivityForApproval() {

    this.pmService.getActivityForApproval(this.user.EmployeeId).subscribe({
      next: (res) => {

        this.activites = res

        console.log("act",res)

      }, error: (err) => {
        console.error(err)
      }
    })
  }
  sidebarToggle() {
    //toggle sidebar function
    this.document.body.classList.toggle('toggle-sidebar');
  }

  routeToApproval (act:ActivityView){
    
    this.router.navigate(['actForApproval',{Activties:act}])
  }

  logOut() {

    this.authGuard.logout();
  }
}

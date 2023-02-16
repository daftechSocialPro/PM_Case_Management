import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SelectList } from '../../common/common';
import { TaskMembers, TaskView } from '../tasks/task';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { IndividualConfig } from 'ngx-toastr';
import { TaskService } from '../tasks/task.service';
import { UserView } from '../../pages-login/user';
import { UserService } from '../../pages-login/user.service';




@Component({
  selector: 'app-activity-parents',
  templateUrl: './activity-parents.component.html',
  styleUrls: ['./activity-parents.component.css']
})
export class ActivityParentsComponent implements OnInit {

  task: TaskView ={};
  taskId: String = "";
  Employees : SelectList[]= [];
  selectedEmployee: SelectList[] = [];
  user!: UserView;
  isUserTaskMember: boolean = false;
  toast!: toastPayload

  constructor(
    private route: ActivatedRoute,
    private taskService: TaskService,
    private userService: UserService,
    private commonService: CommonService,

  ) { }

  ngOnInit(): void {

    this.taskId = this.route.snapshot.paramMap.get('taskId')!
    this.getSingleTask();
    this.ListofEmployees();
    this.user = this.userService.getCurrentUser();
  

  }


  ListofEmployees() {

    this.taskService.getEmployeeNoTaskMembers(this.taskId).subscribe({
      next: (res) => {
        this.Employees = res
      }
      , error: (err) => {
        console.error(err)
      }
    })

  }



  getSingleTask() {

    this.taskService.getSingleTask(this.taskId).subscribe({
      next: (res) => {
        this.task = res
        this.selectedEmployee = []

                         

        if (this.task.TaskMembers!.find(x => x.EmployeeId?.toLowerCase() == this.user.EmployeeId.toLowerCase())) {
          this.isUserTaskMember = true;
        }
      }, error: (err) => {
        console.error(err)
      }
    })

  }

  selectEmployee(event: SelectList) {

    this.selectedEmployee.push(event)
    this.Employees = this.Employees.filter(x => x.Id != event.Id)

  }

  removeSelected(emp: SelectList) {

    this.selectedEmployee = this.selectedEmployee.filter(x => x.Id != emp.Id)
    this.Employees.push(emp)

  }


  AddMembers() {

    let taskMembers: TaskMembers = {
      Employee: this.selectedEmployee,
      TaskId: this.taskId
    }

    this.taskService.addTaskMembers(taskMembers).subscribe({
      next: (res) => {
        this.toast = {
          message: "Members added Successfully",
          title: 'Successfully Added.',
          type: 'success',
          ic: {
            timeOut: 2500,
            closeButton: true,
          } as IndividualConfig,
        };
        this.commonService.showToast(this.toast);
        this.getSingleTask();
        this.ListofEmployees();

      }, error: (err) => {
        this.toast = {
          message: err.message,
          title: 'Network Error.',
          type: 'error',
          ic: {
            timeOut: 2500,
            closeButton: true,
          } as IndividualConfig,
        };
        this.commonService.showToast(this.toast);
        console.error(err)
      }
    })
  }
  getImage(value: string) {
    return this.commonService.createImgPath(value)
  }

  taskMemo(value: string ){

    alert (value)
  }

}

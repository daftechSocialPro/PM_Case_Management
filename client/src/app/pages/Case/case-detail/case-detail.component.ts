import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BeforeSlideDetail } from 'lightgallery/lg-events';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { ConfirmationDialogService } from 'src/app/components/confirmation-dialog/confirmation-dialog.service';
import { UserView } from '../../pages-login/user';
import { UserService } from '../../pages-login/user.service';
import { CaseService } from '../case.service';
import { ICaseView } from '../encode-case/Icase';
import { CompleteCaseComponent } from './complete-case/complete-case.component';
import { MakeAppointmentCaseComponent } from './make-appointment-case/make-appointment-case.component';
import { SendSmsComponent } from './send-sms/send-sms.component';
import { TransferCaseComponent } from './transfer-case/transfer-case.component';
import lgZoom from 'lightgallery/plugins/zoom';

@Component({
  selector: 'app-case-detail',
  templateUrl: './case-detail.component.html',
  styleUrls: ['./case-detail.component.css'],
})
export class CaseDetailComponent implements OnInit {

  settings = {
    counter: false,
    plugins: [lgZoom]
  };
  onBeforeSlide = (detail: BeforeSlideDetail): void => {
    const { index, prevIndex } = detail;
    console.log(index, prevIndex);
  };

  caseHistoryId!: string
  user!: UserView
  caseDetail!: ICaseView
  toast !: toastPayload


  constructor(
    private caseService: CaseService,
    private userService: UserService,
    private router: ActivatedRoute,
    private route: Router,
    private commonService: CommonService,
    private modalService: NgbModal,
    private confirmationDialogService: ConfirmationDialogService
  ) {}
  ngOnInit(): void {

    this.user = this.userService.getCurrentUser()
    this.caseHistoryId = this.router.snapshot.paramMap.get('historyId')!
    this.getCaseDetail()
  
  }

  getCaseDetail() {
    this.caseService
      .GetCaseDetail(this.user.EmployeeId, this.caseHistoryId)
      .subscribe({
        next: (res) => {
          this.caseDetail = res;
          console.log(res);
        },
        error: (err) => {
          console.error(err);
        },
      });
  }

  AddtoWaiting() {
    this.confirmationDialogService
      .confirm(
        'Please confirm..',
        'Do you really want to Add Case to waiting list ?'
      )
      .then((confirmed) => {
        if (confirmed) {
          this.caseService.AddtoWaiting(this.caseHistoryId).subscribe({
            next: (res) => {
              this.toast = {
                message: 'Successfully added to waiting list!!',
                title: 'Successfull.',
                type: 'success',
                ic: {
                  timeOut: 2500,
                  closeButton: true,
                } as IndividualConfig,
              };
              this.commonService.showToast(this.toast);
              this.route.navigate(['mycaselist']);
            },
            error: (err) => {
              this.toast = {
                message: 'Something went wrong!!',
                title: 'Network error.',
                type: 'error',
                ic: {
                  timeOut: 2500,
                  closeButton: true,
                } as IndividualConfig,
              };
              this.commonService.showToast(this.toast);
            },
          });
        }
      })
      .catch(() =>
        console.log(
          'User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'
        )
      );
  }

  SendSMS() {
    let modalRef = this.modalService.open(SendSmsComponent, {
      backdrop: 'static',
    });
    modalRef.componentInstance.historyId = this.caseHistoryId;
  }

  CompleteCase() {
    let modalRef = this.modalService.open(CompleteCaseComponent, {
      backdrop: 'static',
    });
    modalRef.componentInstance.historyId = this.caseHistoryId;
  }

  Revert() {
    this.confirmationDialogService
      .confirm('Please confirm..', 'Do you really want to Revert this Case ?')
      .then((confirmed) => {
        if (confirmed) {
          this.caseService
            .RevertCase({
              CaseHistoryId: this.caseHistoryId,
              EmployeeId: this.user.EmployeeId,
            })
            .subscribe({
              next: (res) => {
                this.toast = {
                  message: 'Case Reverted Successfully!!',
                  title: 'Successfull.',
                  type: 'success',
                  ic: {
                    timeOut: 2500,
                    closeButton: true,
                  } as IndividualConfig,
                };
                this.commonService.showToast(this.toast);

                this.route.navigate(['mycaselist']);
              },
              error: (err) => {
                this.toast = {
                  message: 'Something went wrong!!',
                  title: 'Network error.',
                  type: 'error',
                  ic: {
                    timeOut: 2500,
                    closeButton: true,
                  } as IndividualConfig,
                };
                this.commonService.showToast(this.toast);
              },
            });
        }
      })
      .catch(() =>
        console.log(
          'User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'
        )
      );
  }

  TransferCase() {
    let modalRef = this.modalService.open(TransferCaseComponent, {
      size: 'xl',
      backdrop: 'static',
    });
    modalRef.componentInstance.historyId = this.caseHistoryId;
    modalRef.componentInstance.CaseTypeName = this.caseDetail.CaseTypeName;
    modalRef.componentInstance.CaseTypeId  = this.caseDetail.CaseTypeId
  }

  Appointment() {
    let modalRef = this.modalService.open(MakeAppointmentCaseComponent, {
      size: 'xl',
      backdrop: 'static',
    });
    modalRef.componentInstance.historyId = this.caseHistoryId;
  }

  getImage (value:string){

    return this.commonService.createImgPath(value)
  }


}

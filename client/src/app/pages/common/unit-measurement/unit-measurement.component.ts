import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IndividualConfig } from 'ngx-toastr';
import { CommonService, toastPayload } from 'src/app/common/common.service';
import { OrganizationService } from '../organization/organization.service';
import { AddUpdateMeasurementComponent } from './add-update-measurement/add-update-measurement.component';
import { UnitMeasurment } from './unit-measurment';

@Component({
  selector: 'app-unit-measurement',
  templateUrl: './unit-measurement.component.html',
  styleUrls: ['./unit-measurement.component.css']
})
export class UnitMeasurementComponent {

  unitOfMeasurments: UnitMeasurment[] = [];
  toast!: toastPayload;



  constructor(private orgService: OrganizationService, private commonService: CommonService, private modalService: NgbModal) {
    this.unitOfMeasurmentsList();
  }

  ngOnInit(): void {

    this.unitOfMeasurmentsList();

  }

  unitOfMeasurmentsList() {
    this.orgService.getUnitOfMeasurment().subscribe({
      next: (res) => {
        this.unitOfMeasurments = res
      }, error: (err) => {
        this.toast = {
          message: 'Something went wrong',
          title: 'Network error.',
          type: 'error',
          ic: {
            timeOut: 2500,
            closeButton: true,
          } as IndividualConfig,
        };
        this.commonService.showToast(this.toast);
      }
    })
  }

  addUnitOfMeasurment() {

    this.modalService.open(AddUpdateMeasurementComponent, { size: 'lg', backdrop: 'static' })

  }

  updateUnitOfMeasurment(unit: UnitMeasurment) {

    let modalref = this.modalService.open(AddUpdateMeasurementComponent, { size: 'lg', backdrop: 'static' })

    modalref.componentInstance.UnitOfMeasurment = unit


  }


}

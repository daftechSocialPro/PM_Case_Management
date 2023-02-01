import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdateMeasurementComponent } from './add-update-measurement.component';

describe('AddUpdateMeasurementComponent', () => {
  let component: AddUpdateMeasurementComponent;
  let fixture: ComponentFixture<AddUpdateMeasurementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddUpdateMeasurementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddUpdateMeasurementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

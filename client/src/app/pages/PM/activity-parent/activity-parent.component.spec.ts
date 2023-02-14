import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivityParentComponent } from './activity-parent.component';

describe('ActivityParentComponent', () => {
  let component: ActivityParentComponent;
  let fixture: ComponentFixture<ActivityParentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ActivityParentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActivityParentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

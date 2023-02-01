import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgramBudgetYearComponent } from './program-budget-year.component';

describe('ProgramBudgetYearComponent', () => {
  let component: ProgramBudgetYearComponent;
  let fixture: ComponentFixture<ProgramBudgetYearComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProgramBudgetYearComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProgramBudgetYearComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

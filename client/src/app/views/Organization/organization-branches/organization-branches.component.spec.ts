import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationBranchesComponent } from './organization-branches.component';

describe('OrganizationBranchesComponent', () => {
  let component: OrganizationBranchesComponent;
  let fixture: ComponentFixture<OrganizationBranchesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganizationBranchesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizationBranchesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

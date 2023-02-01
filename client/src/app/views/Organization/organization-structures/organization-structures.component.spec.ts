import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationStructuresComponent } from './organization-structures.component';

describe('OrganizationStructuresComponent', () => {
  let component: OrganizationStructuresComponent;
  let fixture: ComponentFixture<OrganizationStructuresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganizationStructuresComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrganizationStructuresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

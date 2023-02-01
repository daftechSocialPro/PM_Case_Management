import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StructureCreateComponent } from './structure-create.component';

describe('StructureCreateComponent', () => {
  let component: StructureCreateComponent;
  let fixture: ComponentFixture<StructureCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StructureCreateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StructureCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCentreCoutComponent } from './add-centre-cout.component';

describe('AddCentreCoutComponent', () => {
  let component: AddCentreCoutComponent;
  let fixture: ComponentFixture<AddCentreCoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCentreCoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddCentreCoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

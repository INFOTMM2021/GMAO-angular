import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CentreCoutComponent } from './centre-cout.component';

describe('CentreCoutComponent', () => {
  let component: CentreCoutComponent;
  let fixture: ComponentFixture<CentreCoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CentreCoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CentreCoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

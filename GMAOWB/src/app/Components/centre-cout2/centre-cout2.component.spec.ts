import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CentreCout2Component } from './centre-cout2.component';

describe('CentreCout2Component', () => {
  let component: CentreCout2Component;
  let fixture: ComponentFixture<CentreCout2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CentreCout2Component ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CentreCout2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

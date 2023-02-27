import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DemInterv2Component } from './dem-interv2.component';

describe('DemInterv2Component', () => {
  let component: DemInterv2Component;
  let fixture: ComponentFixture<DemInterv2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DemInterv2Component ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DemInterv2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParametrePreventiveComponent } from './parametre-preventive.component';

describe('ParametrePreventiveComponent', () => {
  let component: ParametrePreventiveComponent;
  let fixture: ComponentFixture<ParametrePreventiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParametrePreventiveComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParametrePreventiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

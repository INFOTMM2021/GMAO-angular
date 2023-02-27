import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DemandeTravailComponent } from './demande-travail.component';

describe('DemandeTravailComponent', () => {
  let component: DemandeTravailComponent;
  let fixture: ComponentFixture<DemandeTravailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DemandeTravailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DemandeTravailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

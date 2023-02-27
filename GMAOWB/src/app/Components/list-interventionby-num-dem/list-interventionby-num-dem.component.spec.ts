import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListInterventionbyNumDemComponent } from './list-interventionby-num-dem.component';

describe('ListInterventionbyNumDemComponent', () => {
  let component: ListInterventionbyNumDemComponent;
  let fixture: ComponentFixture<ListInterventionbyNumDemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListInterventionbyNumDemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListInterventionbyNumDemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

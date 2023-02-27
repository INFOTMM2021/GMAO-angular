import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListInterventionComponent } from './list-intervention.component';

describe('ListInterventionComponent', () => {
  let component: ListInterventionComponent;
  let fixture: ComponentFixture<ListInterventionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListInterventionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListInterventionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

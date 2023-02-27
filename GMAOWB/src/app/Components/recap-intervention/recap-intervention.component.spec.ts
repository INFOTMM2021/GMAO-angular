import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecapInterventionComponent } from './recap-intervention.component';

describe('RecapInterventionComponent', () => {
  let component: RecapInterventionComponent;
  let fixture: ComponentFixture<RecapInterventionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecapInterventionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecapInterventionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

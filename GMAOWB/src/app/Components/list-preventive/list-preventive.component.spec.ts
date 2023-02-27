import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPreventiveComponent } from './list-preventive.component';

describe('ListPreventiveComponent', () => {
  let component: ListPreventiveComponent;
  let fixture: ComponentFixture<ListPreventiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListPreventiveComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListPreventiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

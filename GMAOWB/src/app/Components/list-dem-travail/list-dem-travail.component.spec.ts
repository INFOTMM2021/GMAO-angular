import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListDemTravailComponent } from './list-dem-travail.component';

describe('ListDemTravailComponent', () => {
  let component: ListDemTravailComponent;
  let fixture: ComponentFixture<ListDemTravailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListDemTravailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListDemTravailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

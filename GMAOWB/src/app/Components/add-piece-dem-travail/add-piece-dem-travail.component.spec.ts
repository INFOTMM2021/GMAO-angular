import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPieceDemTravailComponent } from './add-piece-dem-travail.component';

describe('AddPieceDemTravailComponent', () => {
  let component: AddPieceDemTravailComponent;
  let fixture: ComponentFixture<AddPieceDemTravailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddPieceDemTravailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddPieceDemTravailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

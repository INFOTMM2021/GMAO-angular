import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainOeuvreComponent } from './main-oeuvre.component';

describe('MainOeuvreComponent', () => {
  let component: MainOeuvreComponent;
  let fixture: ComponentFixture<MainOeuvreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainOeuvreComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainOeuvreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

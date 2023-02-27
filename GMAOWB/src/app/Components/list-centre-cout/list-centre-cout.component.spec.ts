import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCentreCoutComponent } from './list-centre-cout.component';

describe('ListCentreCoutComponent', () => {
  let component: ListCentreCoutComponent;
  let fixture: ComponentFixture<ListCentreCoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListCentreCoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListCentreCoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

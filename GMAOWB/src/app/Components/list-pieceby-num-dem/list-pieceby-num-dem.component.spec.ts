import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPiecebyNumDemComponent } from './list-pieceby-num-dem.component';

describe('ListPiecebyNumDemComponent', () => {
  let component: ListPiecebyNumDemComponent;
  let fixture: ComponentFixture<ListPiecebyNumDemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListPiecebyNumDemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListPiecebyNumDemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

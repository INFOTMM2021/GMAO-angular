import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegulStockComponent } from './regul-stock.component';

describe('RegulStockComponent', () => {
  let component: RegulStockComponent;
  let fixture: ComponentFixture<RegulStockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegulStockComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegulStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

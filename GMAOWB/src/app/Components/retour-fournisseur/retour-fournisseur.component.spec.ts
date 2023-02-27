import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RetourFournisseurComponent } from './retour-fournisseur.component';

describe('RetourFournisseurComponent', () => {
  let component: RetourFournisseurComponent;
  let fixture: ComponentFixture<RetourFournisseurComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RetourFournisseurComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RetourFournisseurComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

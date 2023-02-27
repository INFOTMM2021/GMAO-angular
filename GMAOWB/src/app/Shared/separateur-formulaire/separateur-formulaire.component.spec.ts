import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeparateurFormulaireComponent } from './separateur-formulaire.component';

describe('SeparateurFormulaireComponent', () => {
  let component: SeparateurFormulaireComponent;
  let fixture: ComponentFixture<SeparateurFormulaireComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeparateurFormulaireComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SeparateurFormulaireComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

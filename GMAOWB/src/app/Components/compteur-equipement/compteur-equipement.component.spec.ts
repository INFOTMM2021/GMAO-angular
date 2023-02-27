import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompteurEquipementComponent } from './compteur-equipement.component';

describe('CompteurEquipementComponent', () => {
  let component: CompteurEquipementComponent;
  let fixture: ComponentFixture<CompteurEquipementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompteurEquipementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompteurEquipementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

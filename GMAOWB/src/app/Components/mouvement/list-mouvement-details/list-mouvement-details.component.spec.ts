import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListMouvementDetailsComponent } from './list-mouvement-details.component';

describe('ListMouvementDetailsComponent', () => {
  let component: ListMouvementDetailsComponent;
  let fixture: ComponentFixture<ListMouvementDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListMouvementDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListMouvementDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DDTreeComponent } from './ddtree.component';

describe('DDTreeComponent', () => {
  let component: DDTreeComponent;
  let fixture: ComponentFixture<DDTreeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DDTreeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DDTreeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

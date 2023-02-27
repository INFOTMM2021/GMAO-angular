import { TestBed } from '@angular/core/testing';

import { DemTravailService } from './dem-travail.service';

describe('DemTravailService', () => {
  let service: DemTravailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DemTravailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

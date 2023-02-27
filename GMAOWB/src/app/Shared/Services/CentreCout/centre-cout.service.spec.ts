import { TestBed } from '@angular/core/testing';

import { CentreCoutService } from './centre-cout.service';

describe('CentreCoutService', () => {
  let service: CentreCoutService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CentreCoutService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
